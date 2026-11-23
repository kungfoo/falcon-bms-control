use std::fs;
use std::io;
use std::path::{Path, PathBuf};

use crate::settings::Settings;

const SETTINGS_FILE_NAME: &str = "settings.toml";

/// Resolve the platform-appropriate settings file path.
pub fn default_settings_path() -> Option<PathBuf> {
    #[cfg(any(target_os = "linux", target_os = "windows", target_os = "macos"))]
    {
        directories::ProjectDirs::from("com", "falcon-bms-control", "falcon-bms-control")
            .map(|dirs| dirs.config_dir().join(SETTINGS_FILE_NAME))
    }

    #[cfg(target_os = "android")]
    {
        bevy::android::ANDROID_APP
            .get()
            .and_then(|app| app.internal_data_path())
            .map(|dir| dir.join(SETTINGS_FILE_NAME))
    }

    #[cfg(target_os = "ios")]
    {
        std::env::var_os("HOME").map(|home| {
            PathBuf::from(home)
                .join("Library")
                .join("Application Support")
                .join("falcon-bms-control")
                .join(SETTINGS_FILE_NAME)
        })
    }

    #[cfg(not(any(
        target_os = "linux",
        target_os = "windows",
        target_os = "macos",
        target_os = "android",
        target_os = "ios"
    )))]
    {
        None
    }
}

pub fn load_from_path(path: &Path) -> io::Result<Settings> {
    let contents = fs::read_to_string(path)?;
    toml::from_str(&contents).map_err(|e| io::Error::new(io::ErrorKind::InvalidData, e))
}

pub fn save_to_path(path: &Path, settings: &Settings) -> io::Result<()> {
    if let Some(parent) = path.parent() {
        fs::create_dir_all(parent)?;
    }
    let contents = toml::to_string_pretty(settings)
        .map_err(|e| io::Error::new(io::ErrorKind::InvalidData, e))?;
    fs::write(path, contents)
}

#[cfg(test)]
mod tests {
    use super::*;
    use tempfile::tempdir;

    #[test]
    fn save_and_load_preserves_false_booleans_and_all_fields() {
        let dir = tempdir().unwrap();
        let path = dir.path().join("nested").join("settings.toml");

        let custom = Settings {
            refresh_rate: 60,
            quality: 85,
            vibrate: true,
            ip: Some("192.168.1.42".into()),
            dark_mode: true,
            layout: "cockpit-like".into(),
            fullscreen: false,
        };

        save_to_path(&path, &custom).expect("saving settings should succeed");
        let loaded = load_from_path(&path).expect("loading settings should succeed");

        assert_eq!(loaded, custom);
        assert!(!loaded.fullscreen);
    }

    #[test]
    fn missing_fields_in_toml_fall_back_to_defaults() {
        let dir = tempdir().unwrap();
        let path = dir.path().join("settings.toml");
        fs::write(&path, "fullscreen = false\n").unwrap();

        let loaded = load_from_path(&path).unwrap();
        let defaults = Settings::default();

        assert!(!loaded.fullscreen);
        assert_eq!(loaded.refresh_rate, defaults.refresh_rate);
        assert_eq!(loaded.quality, defaults.quality);
        assert_eq!(loaded.layout, defaults.layout);
        assert_eq!(loaded.ip, None);
    }

    #[test]
    fn corrupt_file_returns_error() {
        let dir = tempdir().unwrap();
        let path = dir.path().join("settings.toml");
        fs::write(&path, "not valid toml = = =").unwrap();

        assert!(load_from_path(&path).is_err());
    }

    #[test]
    fn desktop_default_settings_path_is_available() {
        let path = default_settings_path();
        assert!(path.is_some());
        assert_eq!(
            path.unwrap().file_name().and_then(|n| n.to_str()),
            Some(SETTINGS_FILE_NAME)
        );
    }
}
