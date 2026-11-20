use serde::{Deserialize, Serialize};

/// ENet channel mapping matching `falcon-bms-control-server/src/texture_reader.rs`.
#[derive(Debug, Clone, Copy, PartialEq, Eq, Hash)]
#[repr(u8)]
pub enum TextureChannel {
    LeftMfd = 1,
    RightMfd = 2,
    Ded = 3,
    Rwr = 4,
}

impl TextureChannel {
    pub fn from_channel_id(channel: u8) -> Option<Self> {
        match channel {
            1 => Some(Self::LeftMfd),
            2 => Some(Self::RightMfd),
            3 => Some(Self::Ded),
            4 => Some(Self::Rwr),
            _ => None,
        }
    }

    pub fn from_identifier(identifier: &str) -> Option<Self> {
        match identifier {
            "f16/left-mfd" => Some(Self::LeftMfd),
            "f16/right-mfd" => Some(Self::RightMfd),
            "f16/ded" => Some(Self::Ded),
            "f16/rwr" => Some(Self::Rwr),
            _ => None,
        }
    }

    pub fn identifier(self) -> &'static str {
        match self {
            Self::LeftMfd => "f16/left-mfd",
            Self::RightMfd => "f16/right-mfd",
            Self::Ded => "f16/ded",
            Self::Rwr => "f16/rwr",
        }
    }
}

/// Wire protocol messages matching `falcon-bms-control-server/src/msgpack.rs`.
#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize)]
#[serde(tag = "type")]
pub enum ProtocolMessage {
    #[serde(rename = "hello")]
    Hello {},
    #[serde(rename = "ack")]
    Ack {},
    #[serde(rename = "icp-pressed")]
    IcpButtonPressed {
        icp: Option<String>,
        button: String,
    },
    #[serde(rename = "icp-released")]
    IcpButtonReleased {
        icp: Option<String>,
        button: String,
    },
    #[serde(rename = "osb-pressed")]
    OsbButtonPressed { mfd: String, osb: String },
    #[serde(rename = "osb-released")]
    OsbButtonReleased { mfd: String, osb: String },
    #[serde(rename = "streamed-texture")]
    StreamedTextureRequest {
        identifier: String,
        command: Command,
        refresh_rate: Option<u16>,
        quality: Option<u16>,
    },
    Unknown,
}

#[derive(Debug, Clone, Copy, PartialEq, Eq, Serialize, Deserialize)]
pub enum Command {
    #[serde(rename = "start")]
    Start,
    #[serde(rename = "stop")]
    Stop,
}

impl ProtocolMessage {
    /// Encode as a msgpack named map so the server's `#[serde(tag = "type")]` decoder can parse it.
    pub fn to_msgpack(&self) -> Result<Vec<u8>, rmp_serde::encode::Error> {
        rmp_serde::to_vec_named(self)
    }

    pub fn from_msgpack(bytes: &[u8]) -> Result<Self, rmp_serde::decode::Error> {
        rmp_serde::from_slice(bytes)
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn roundtrip_hello_and_ack() {
        for msg in [ProtocolMessage::Hello {}, ProtocolMessage::Ack {}] {
            let encoded = msg.to_msgpack().expect("encode should succeed");
            let decoded = ProtocolMessage::from_msgpack(&encoded).expect("decode should succeed");
            assert_eq!(decoded, msg);
        }
    }

    #[test]
    fn roundtrip_osb_buttons() {
        let pressed = ProtocolMessage::OsbButtonPressed {
            mfd: "f16/left-mfd".into(),
            osb: "OSB_1".into(),
        };
        let released = ProtocolMessage::OsbButtonReleased {
            mfd: "f16/right-mfd".into(),
            osb: "OSB_20".into(),
        };
        for msg in [pressed, released] {
            let bytes = msg.to_msgpack().unwrap();
            assert_eq!(ProtocolMessage::from_msgpack(&bytes).unwrap(), msg);
        }
    }

    #[test]
    fn roundtrip_icp_buttons() {
        let pressed = ProtocolMessage::IcpButtonPressed {
            icp: Some("f16/icp".into()),
            button: "COM1".into(),
        };
        let released = ProtocolMessage::IcpButtonReleased {
            icp: None,
            button: "icp-wpt-next".into(),
        };
        for msg in [pressed, released] {
            let bytes = msg.to_msgpack().unwrap();
            assert_eq!(ProtocolMessage::from_msgpack(&bytes).unwrap(), msg);
        }
    }

    #[test]
    fn roundtrip_streamed_texture_request() {
        let start = ProtocolMessage::StreamedTextureRequest {
            identifier: "f16/left-mfd".into(),
            command: Command::Start,
            refresh_rate: Some(30),
            quality: Some(70),
        };
        let stop = ProtocolMessage::StreamedTextureRequest {
            identifier: "f16/left-mfd".into(),
            command: Command::Stop,
            refresh_rate: None,
            quality: None,
        };
        for msg in [start, stop] {
            let bytes = msg.to_msgpack().unwrap();
            assert_eq!(ProtocolMessage::from_msgpack(&bytes).unwrap(), msg);
        }
    }

    #[test]
    fn texture_channel_mappings_are_consistent() {
        for channel in [
            TextureChannel::LeftMfd,
            TextureChannel::RightMfd,
            TextureChannel::Ded,
            TextureChannel::Rwr,
        ] {
            assert_eq!(TextureChannel::from_channel_id(channel as u8), Some(channel));
            assert_eq!(
                TextureChannel::from_identifier(channel.identifier()),
                Some(channel)
            );
        }
        assert_eq!(TextureChannel::from_channel_id(0), None);
        assert_eq!(TextureChannel::from_channel_id(99), None);
        assert_eq!(TextureChannel::from_identifier("unknown"), None);
    }
}
