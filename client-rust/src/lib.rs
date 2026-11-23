pub mod layouts;
pub mod network;
pub mod protocol;
pub mod settings;
pub mod ui;

use std::time::Duration;

use bevy::prelude::*;
use bevy::window::{MonitorSelection, WindowMode, WindowResolution};
use bevy::winit::{UpdateMode, WinitSettings};

pub fn build_app() -> App {
    let mut app = App::new();
    app.add_plugins(DefaultPlugins.set(WindowPlugin {
        primary_window: Some(Window {
            title: "Falcon BMS Control".into(),
            // Use BorderlessFullscreen on mobile so Bevy adopts the exact device viewport
            #[cfg(any(target_os = "android", target_os = "ios"))]
            mode: WindowMode::BorderlessFullscreen(MonitorSelection::Current),
            #[cfg(not(any(target_os = "android", target_os = "ios")))]
            resolution: WindowResolution::new(1280, 720),
            // Do NOT set restrictive resize_constraints on mobile
            ..default()
        }),
        ..default()
    }))
    .insert_resource(WinitSettings {
        focused_mode: UpdateMode::reactive(Duration::from_millis(16)),
        unfocused_mode: UpdateMode::reactive_low_power(Duration::from_millis(100)),
    })
    .init_resource::<layouts::LayoutRegistry>()
    .add_plugins((
        settings::SettingsPlugin,
        network::NetworkPlugin,
        ui::UiPlugin,
    ));
    app
}

#[bevy_main]
pub fn main() {
    build_app().run();
}

#[cfg(target_os = "ios")]
#[unsafe(no_mangle)] // use #[no_mangle] if on Rust 2021 edition
pub extern "C" fn ios_main() {
    build_app().run();
}
