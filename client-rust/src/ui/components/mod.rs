pub mod ded;
pub mod fit;
pub mod icp;
pub mod mfd;
pub mod rwr;

use bevy::prelude::*;

use crate::network::NetworkClient;
use crate::protocol::ProtocolMessage;
use crate::ui::ThemeColors;

/// Preloaded button click sounds reused across all cockpit buttons.
#[derive(Resource)]
pub struct ButtonSounds {
    pub pressed: Handle<AudioSource>,
    pub released: Handle<AudioSource>,
}

/// Interactive cockpit button that emits press/release `ProtocolMessage`s and highlights while held.
#[derive(Component, Debug, Clone)]
pub struct CockpitButton {
    pub on_press: ProtocolMessage,
    pub on_release: ProtocolMessage,
    pub is_pressed: bool,
}

impl CockpitButton {
    pub fn osb(mfd: &str, osb: &str) -> Self {
        Self {
            on_press: ProtocolMessage::OsbButtonPressed {
                mfd: mfd.to_string(),
                osb: osb.to_string(),
            },
            on_release: ProtocolMessage::OsbButtonReleased {
                mfd: mfd.to_string(),
                osb: osb.to_string(),
            },
            is_pressed: false,
        }
    }

    pub fn icp(icp: Option<&str>, button: &str) -> Self {
        Self {
            on_press: ProtocolMessage::IcpButtonPressed {
                icp: icp.map(ToString::to_string),
                button: button.to_string(),
            },
            on_release: ProtocolMessage::IcpButtonReleased {
                icp: icp.map(ToString::to_string),
                button: button.to_string(),
            },
            is_pressed: false,
        }
    }
}

pub fn load_button_sounds(mut commands: Commands, asset_server: Res<AssetServer>) {
    commands.insert_resource(ButtonSounds {
        pressed: asset_server.load("sounds/A/button-pressed.wav"),
        released: asset_server.load("sounds/A/button-released.wav"),
    });
}

pub fn on_cockpit_button_press(
    trigger: On<PointerPress>,
    mut commands: Commands,
    mut buttons: Query<(&mut CockpitButton, &mut BackgroundColor)>,
    client: Res<NetworkClient>,
    sounds: Option<Res<ButtonSounds>>,
) {
    if let Ok((mut btn, mut bg)) = buttons.get_mut(trigger.entity)
        && !btn.is_pressed
    {
        btn.is_pressed = true;
        *bg = BackgroundColor(ThemeColors::BUTTON_PRESSED);
        client.send(btn.on_press.clone());
        if let Some(sounds) = sounds {
            commands.spawn((
                AudioPlayer::new(sounds.pressed.clone()),
                PlaybackSettings::DESPAWN,
            ));
        }
    }
}

pub fn on_cockpit_button_release(
    trigger: On<PointerRelease>,
    mut commands: Commands,
    mut buttons: Query<(&mut CockpitButton, &mut BackgroundColor)>,
    client: Res<NetworkClient>,
    sounds: Option<Res<ButtonSounds>>,
) {
    release_button(
        trigger.entity,
        &mut commands,
        &mut buttons,
        &client,
        sounds.as_deref(),
    );
}

pub fn on_cockpit_button_leave(
    trigger: On<PointerLeave>,
    mut commands: Commands,
    mut buttons: Query<(&mut CockpitButton, &mut BackgroundColor)>,
    client: Res<NetworkClient>,
    sounds: Option<Res<ButtonSounds>>,
) {
    release_button(
        trigger.entity,
        &mut commands,
        &mut buttons,
        &client,
        sounds.as_deref(),
    );
}

pub fn on_cockpit_button_cancel(
    trigger: On<PointerCancel>,
    mut commands: Commands,
    mut buttons: Query<(&mut CockpitButton, &mut BackgroundColor)>,
    client: Res<NetworkClient>,
    sounds: Option<Res<ButtonSounds>>,
) {
    release_button(
        trigger.entity,
        &mut commands,
        &mut buttons,
        &client,
        sounds.as_deref(),
    );
}

fn release_button(
    entity: Entity,
    commands: &mut Commands,
    buttons: &mut Query<(&mut CockpitButton, &mut BackgroundColor)>,
    client: &NetworkClient,
    sounds: Option<&ButtonSounds>,
) {
    if let Ok((mut btn, mut bg)) = buttons.get_mut(entity)
        && btn.is_pressed
    {
        btn.is_pressed = false;
        *bg = BackgroundColor(ThemeColors::BUTTON);
        client.send(btn.on_release.clone());
        if let Some(sounds) = sounds {
            commands.spawn((
                AudioPlayer::new(sounds.released.clone()),
                PlaybackSettings::DESPAWN,
            ));
        }
    }
}

/// Helper to attach all press/release/leave/cancel observers to a `CockpitButton` entity.
pub fn attach_button_observers(entity: &mut EntityCommands) {
    entity
        .observe(on_cockpit_button_press)
        .observe(on_cockpit_button_release)
        .observe(on_cockpit_button_leave)
        .observe(on_cockpit_button_cancel);
}
