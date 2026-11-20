pub mod layouts;
pub mod network;
pub mod protocol;
pub mod settings;
pub mod ui;

use std::time::Duration;

use bevy::prelude::*;
use bevy::winit::{UpdateMode, WinitSettings};

pub fn build_app() -> App {
    let mut app = App::new();
    app.add_plugins(DefaultPlugins.set(WindowPlugin {
        primary_window: Some(Window {
            title: "Falcon BMS Control".into(),
            resizable: true,
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

pub fn run() {
    build_app().run();
}
