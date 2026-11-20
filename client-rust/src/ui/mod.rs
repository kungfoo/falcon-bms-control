pub mod cockpit;
pub mod components;
pub mod connecting;
pub mod settings;

use bevy::prelude::*;

#[derive(States, Debug, Clone, Copy, PartialEq, Eq, Hash, Default)]
pub enum AppState {
    #[default]
    Connecting,
    Cockpit,
    Settings,
}

#[derive(Resource, Debug, Clone, Copy, Default)]
pub struct PreviousState(pub AppState);

pub struct ThemeColors;

impl ThemeColors {
    pub const BACKGROUND: Color = Color::srgb(0.05, 0.05, 0.06);
    pub const PANEL: Color = Color::srgb(0.14, 0.14, 0.16);
    pub const BUTTON: Color = Color::srgb(0.22, 0.22, 0.25);
    pub const BUTTON_PRESSED: Color = Color::srgb(0.15, 0.55, 0.25);
    pub const ACCENT_GREEN: Color = Color::srgb(0.2, 0.8, 0.3);
    pub const CYAN_OUTLINE: Color = Color::srgb(0.2, 0.75, 0.85);
    pub const RWR_RETICLE: Color = Color::srgba(0.4, 0.6, 0.4, 0.65);
    pub const TEXT: Color = Color::srgb(0.92, 0.92, 0.92);
    pub const MUTED_TEXT: Color = Color::srgb(0.65, 0.65, 0.68);
    pub const BLACK: Color = Color::BLACK;
}

pub struct UiPlugin;

impl Plugin for UiPlugin {
    fn build(&self, app: &mut App) {
        app.init_state::<AppState>()
            .init_resource::<PreviousState>()
            .add_plugins((
                connecting::ConnectingScreenPlugin,
                cockpit::CockpitScreenPlugin,
                settings::SettingsScreenPlugin,
            ))
            .add_systems(Startup, setup_camera)
            .add_systems(Update, handle_escape_quit);
    }
}

fn setup_camera(mut commands: Commands) {
    commands.spawn(Camera2d);
}

fn handle_escape_quit(keys: Res<ButtonInput<KeyCode>>, mut exit: MessageWriter<AppExit>) {
    if keys.just_pressed(KeyCode::Escape) {
        exit.write(AppExit::Success);
    }
}
