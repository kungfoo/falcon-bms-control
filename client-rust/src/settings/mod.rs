pub mod storage;

use std::net::Ipv4Addr;
use std::path::PathBuf;

use bevy::prelude::*;
use bevy::window::{PrimaryWindow, WindowMode};
use serde::{Deserialize, Serialize};

fn default_refresh_rate() -> u16 {
    30
}

fn default_quality() -> u16 {
    70
}

fn default_layout() -> String {
    "default-landscape".to_string()
}

fn default_fullscreen() -> bool {
    true
}

#[derive(Debug, Clone, PartialEq, Eq, Serialize, Deserialize, Resource)]
pub struct Settings {
    #[serde(default = "default_refresh_rate")]
    pub refresh_rate: u16,
    #[serde(default = "default_quality")]
    pub quality: u16,
    #[serde(default)]
    pub vibrate: bool,
    #[serde(default)]
    pub ip: Option<String>,
    #[serde(default)]
    pub dark_mode: bool,
    #[serde(default = "default_layout")]
    pub layout: String,
    #[serde(default = "default_fullscreen")]
    pub fullscreen: bool,
}

impl Default for Settings {
    fn default() -> Self {
        Self {
            refresh_rate: default_refresh_rate(),
            quality: default_quality(),
            vibrate: false,
            ip: None,
            dark_mode: false,
            layout: default_layout(),
            fullscreen: default_fullscreen(),
        }
    }
}

impl Settings {
    pub fn set_ip(&mut self, value: Option<String>) -> Result<(), &'static str> {
        match value {
            None => {
                self.ip = None;
                Ok(())
            }
            Some(ref s) if s.trim().is_empty() => {
                self.ip = None;
                Ok(())
            }
            Some(s) => {
                let trimmed = s.trim();
                if is_valid_ipv4(trimmed) {
                    self.ip = Some(trimmed.to_string());
                    Ok(())
                } else {
                    Err("Invalid IPv4 address")
                }
            }
        }
    }
}

pub fn is_valid_ipv4(value: &str) -> bool {
    value.parse::<Ipv4Addr>().is_ok()
}

#[derive(Resource, Debug, Clone)]
pub struct SettingsPath(pub Option<PathBuf>);

pub struct SettingsPlugin;

impl Plugin for SettingsPlugin {
    fn build(&self, app: &mut App) {
        let path = storage::default_settings_path();
        let settings = match &path {
            Some(p) => match storage::load_from_path(p) {
                Ok(loaded) => {
                    info!("Loaded settings from {:?}", p);
                    loaded
                }
                Err(e) if e.kind() == std::io::ErrorKind::NotFound => {
                    info!("No settings file found at {:?}, using defaults", p);
                    Settings::default()
                }
                Err(e) => {
                    warn!("Failed to load settings from {:?} ({}), using defaults", p, e);
                    Settings::default()
                }
            },
            None => {
                warn!("No settings path available for this platform, using defaults");
                Settings::default()
            }
        };

        app.insert_resource(SettingsPath(path))
            .insert_resource(settings)
            .add_systems(Startup, apply_fullscreen_setting)
            .add_systems(
                Update,
                (persist_settings_on_change, apply_fullscreen_on_change),
            );
    }
}

fn persist_settings_on_change(settings: Res<Settings>, path: Res<SettingsPath>) {
    if !settings.is_changed() || settings.is_added() {
        return;
    }
    if let Some(ref path) = path.0
        && let Err(e) = storage::save_to_path(path, &settings)
    {
        error!("Failed to save settings to {:?}: {}", path, e);
    }
}

fn apply_fullscreen_setting(
    settings: Res<Settings>,
    mut windows: Query<&mut Window, With<PrimaryWindow>>,
) {
    apply_fullscreen_to_window(settings.fullscreen, &mut windows);
}

fn apply_fullscreen_on_change(
    settings: Res<Settings>,
    mut windows: Query<&mut Window, With<PrimaryWindow>>,
) {
    if !settings.is_changed() {
        return;
    }
    apply_fullscreen_to_window(settings.fullscreen, &mut windows);
}

fn apply_fullscreen_to_window(
    fullscreen: bool,
    windows: &mut Query<&mut Window, With<PrimaryWindow>>,
) {
    #[cfg(not(any(target_os = "android", target_os = "ios")))]
    {
        if let Ok(mut window) = windows.single_mut() {
            let desired = if fullscreen {
                WindowMode::BorderlessFullscreen(MonitorSelection::Current)
            } else {
                WindowMode::Windowed
            };
            if window.mode != desired {
                window.mode = desired;
            }
        }
    }
    #[cfg(any(target_os = "android", target_os = "ios"))]
    {
        let _ = (fullscreen, windows);
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn ipv4_validation_accepts_valid_and_rejects_invalid() {
        assert!(is_valid_ipv4("127.0.0.1"));
        assert!(is_valid_ipv4("192.168.0.100"));
        assert!(!is_valid_ipv4(""));
        assert!(!is_valid_ipv4("999.0.0.1"));
        assert!(!is_valid_ipv4("192.168.1"));
        assert!(!is_valid_ipv4("not-an-ip"));
    }

    #[test]
    fn set_ip_clears_on_empty_and_rejects_invalid() {
        let mut settings = Settings::default();
        assert!(settings.set_ip(Some("10.0.0.5".into())).is_ok());
        assert_eq!(settings.ip.as_deref(), Some("10.0.0.5"));

        assert!(settings.set_ip(Some("   ".into())).is_ok());
        assert_eq!(settings.ip, None);

        assert!(settings.set_ip(Some("300.1.1.1".into())).is_err());
        assert_eq!(settings.ip, None);
    }
}
