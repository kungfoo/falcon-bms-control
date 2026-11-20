use std::collections::HashMap;
use std::sync::{Arc, Condvar, Mutex};
use std::thread;

use bevy::asset::RenderAssetUsages;
use bevy::prelude::*;
use bevy::render::render_resource::{Extent3d, TextureDimension, TextureFormat};
use crossbeam_channel::{Receiver, Sender, unbounded};

use crate::protocol::TextureChannel;

/// Decoded RGBA8 frame ready to upload into a Bevy `Image`.
#[derive(Debug, Clone)]
pub struct DecodedFrame {
    pub channel: TextureChannel,
    pub width: u32,
    pub height: u32,
    pub rgba: Vec<u8>,
}

/// Pure-Rust JPEG decode helper (mobile-safe, no native C libturbojpeg required).
pub fn decode_jpeg_to_rgba(jpeg_bytes: &[u8]) -> Result<(u32, u32, Vec<u8>), image::ImageError> {
    let img = image::load_from_memory_with_format(jpeg_bytes, image::ImageFormat::Jpeg)?;
    let rgba = img.to_rgba8();
    let (width, height) = rgba.dimensions();
    Ok((width, height, rgba.into_raw()))
}

/// Shared latest-only mailbox so if JPEG decoding falls behind, older frames for the same
/// channel are overwritten instead of piling up latency.
#[derive(Default)]
struct LatestFrameMailbox {
    pending: Mutex<HashMap<TextureChannel, Vec<u8>>>,
    closed: Mutex<bool>,
    notify: Condvar,
}

impl LatestFrameMailbox {
    fn push(&self, channel: TextureChannel, jpeg_bytes: Vec<u8>) {
        if let Ok(mut pending) = self.pending.lock() {
            pending.insert(channel, jpeg_bytes);
            self.notify.notify_one();
        }
    }

    fn take_batch(&self) -> Option<Vec<(TextureChannel, Vec<u8>)>> {
        let mut pending = self.pending.lock().ok()?;
        loop {
            if !pending.is_empty() {
                return Some(pending.drain().collect());
            }
            if *self.closed.lock().ok()? {
                return None;
            }
            pending = self.notify.wait(pending).ok()?;
        }
    }

    fn close(&self) {
        if let Ok(mut closed) = self.closed.lock() {
            *closed = true;
            self.notify.notify_all();
        }
    }
}

/// Background JPEG decoder worker.
pub struct TextureDecoder {
    mailbox: Arc<LatestFrameMailbox>,
    rx: Receiver<DecodedFrame>,
}

impl TextureDecoder {
    pub fn spawn() -> Self {
        let mailbox = Arc::new(LatestFrameMailbox::default());
        let worker_mailbox = Arc::clone(&mailbox);
        let (tx, rx): (Sender<DecodedFrame>, Receiver<DecodedFrame>) = unbounded();

        thread::Builder::new()
            .name("texture-decoder".into())
            .spawn(move || {
                while let Some(batch) = worker_mailbox.take_batch() {
                    for (channel, jpeg_bytes) in batch {
                        match decode_jpeg_to_rgba(&jpeg_bytes) {
                            Ok((width, height, rgba)) => {
                                let _ = tx.send(DecodedFrame {
                                    channel,
                                    width,
                                    height,
                                    rgba,
                                });
                            }
                            Err(e) => {
                                warn!("Failed to decode JPEG for {:?}: {}", channel, e);
                            }
                        }
                    }
                }
            })
            .expect("failed to spawn texture-decoder thread");

        Self { mailbox, rx }
    }

    pub fn submit(&self, channel: TextureChannel, jpeg_bytes: Vec<u8>) {
        self.mailbox.push(channel, jpeg_bytes);
    }

    pub fn try_recv(&self) -> Option<DecodedFrame> {
        self.rx.try_recv().ok()
    }
}

impl Drop for TextureDecoder {
    fn drop(&mut self) {
        self.mailbox.close();
    }
}

/// Holds persistent Bevy `Handle<Image>` assets for each streamed cockpit device.
#[derive(Resource)]
pub struct StreamedTextures {
    handles: HashMap<TextureChannel, Handle<Image>>,
    has_frame: HashMap<TextureChannel, bool>,
}

impl StreamedTextures {
    pub fn new(images: &mut Assets<Image>) -> Self {
        let mut handles = HashMap::new();
        let mut has_frame = HashMap::new();

        for channel in [
            TextureChannel::LeftMfd,
            TextureChannel::RightMfd,
            TextureChannel::Ded,
            TextureChannel::Rwr,
        ] {
            let placeholder = Image::new_fill(
                Extent3d {
                    width: 2,
                    height: 2,
                    depth_or_array_layers: 1,
                },
                TextureDimension::D2,
                &[0, 0, 0, 255],
                TextureFormat::Rgba8UnormSrgb,
                RenderAssetUsages::MAIN_WORLD | RenderAssetUsages::RENDER_WORLD,
            );
            handles.insert(channel, images.add(placeholder));
            has_frame.insert(channel, false);
        }

        Self { handles, has_frame }
    }

    pub fn handle(&self, channel: TextureChannel) -> Handle<Image> {
        self.handles[&channel].clone()
    }

    pub fn has_received_frame(&self, channel: TextureChannel) -> bool {
        self.has_frame.get(&channel).copied().unwrap_or(false)
    }

    pub fn apply_frame(&mut self, images: &mut Assets<Image>, frame: DecodedFrame) {
        if let Some(handle) = self.handles.get(&frame.channel)
            && let Some(mut image) = images.get_mut(handle)
        {
            let size = Extent3d {
                width: frame.width,
                height: frame.height,
                depth_or_array_layers: 1,
            };
            if image.texture_descriptor.size != size {
                *image = Image::new(
                    size,
                    TextureDimension::D2,
                    frame.rgba,
                    TextureFormat::Rgba8UnormSrgb,
                    RenderAssetUsages::MAIN_WORLD | RenderAssetUsages::RENDER_WORLD,
                );
            } else {
                image.data = Some(frame.rgba);
            }
            self.has_frame.insert(frame.channel, true);
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    use std::io::Cursor;

    fn make_test_jpeg(width: u32, height: u32, rgb: [u8; 3]) -> Vec<u8> {
        let mut img = image::RgbImage::new(width, height);
        for pixel in img.pixels_mut() {
            *pixel = image::Rgb(rgb);
        }
        let mut buf = Cursor::new(Vec::new());
        img.write_to(&mut buf, image::ImageFormat::Jpeg).unwrap();
        buf.into_inner()
    }

    #[test]
    fn decode_valid_jpeg_returns_expected_dimensions_and_rgba() {
        let jpeg = make_test_jpeg(16, 8, [200, 50, 20]);
        let (w, h, rgba) = decode_jpeg_to_rgba(&jpeg).expect("valid jpeg should decode");
        assert_eq!((w, h), (16, 8));
        assert_eq!(rgba.len(), (16 * 8 * 4) as usize);
        assert_eq!(rgba[3], 255); // opaque alpha
    }

    #[test]
    fn decode_invalid_jpeg_returns_error() {
        assert!(decode_jpeg_to_rgba(b"not a jpeg").is_err());
    }

    #[test]
    fn mailbox_keeps_only_latest_frame_per_channel() {
        let mailbox = LatestFrameMailbox::default();
        mailbox.push(TextureChannel::LeftMfd, vec![1, 2, 3]);
        mailbox.push(TextureChannel::LeftMfd, vec![4, 5, 6]);
        mailbox.push(TextureChannel::RightMfd, vec![7, 8]);

        let batch = mailbox.take_batch().unwrap();
        let map: HashMap<_, _> = batch.into_iter().collect();
        assert_eq!(map.len(), 2);
        assert_eq!(map[&TextureChannel::LeftMfd], vec![4, 5, 6]);
        assert_eq!(map[&TextureChannel::RightMfd], vec![7, 8]);
    }
}
