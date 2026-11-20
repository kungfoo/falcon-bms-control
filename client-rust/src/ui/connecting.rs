use bevy::prelude::*;
use bevy::text::{FontSize, FontSource};

use crate::network::ConnectionStatus;
use crate::ui::{AppState, PreviousState, ThemeColors};

#[derive(Component)]
struct ConnectingScreenRoot;

#[derive(Component)]
struct ConnectionStatusText;

pub struct ConnectingScreenPlugin;

impl Plugin for ConnectingScreenPlugin {
    fn build(&self, app: &mut App) {
        app.add_systems(OnEnter(AppState::Connecting), spawn_connecting_screen)
            .add_systems(OnExit(AppState::Connecting), despawn_connecting_screen)
            .add_systems(
                Update,
                update_status_text.run_if(in_state(AppState::Connecting)),
            );
    }
}

fn spawn_connecting_screen(
    mut commands: Commands,
    asset_server: Res<AssetServer>,
    status: Res<ConnectionStatus>,
) {
    let font = asset_server.load("fonts/b612/B612Mono-Regular.ttf");
    let settings_icon = asset_server.load("icons/settings.png");

    commands
        .spawn((
            ConnectingScreenRoot,
            Node {
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                padding: UiRect::all(Val::Px(16.0)),
                flex_direction: FlexDirection::Column,
                justify_content: JustifyContent::SpaceBetween,
                align_items: AlignItems::Stretch,
                ..default()
            },
            BackgroundColor(ThemeColors::BACKGROUND),
        ))
        .with_children(|root| {
            // Centered connection status message
            root.spawn(Node {
                width: Val::Percent(100.0),
                flex_grow: 1.0,
                justify_content: JustifyContent::Center,
                align_items: AlignItems::Center,
                ..default()
            })
            .with_children(|center| {
                center.spawn((
                    ConnectionStatusText,
                    Text::new(status.display_text()),
                    TextFont {
                        font: FontSource::Handle(font),
                        font_size: FontSize::Px(28.0),
                        ..default()
                    },
                    TextColor(ThemeColors::TEXT),
                ));
            });

            // Bottom bar with right-aligned settings icon button
            root.spawn(Node {
                width: Val::Percent(100.0),
                height: Val::Px(56.0),
                justify_content: JustifyContent::FlexEnd,
                align_items: AlignItems::Center,
                ..default()
            })
            .with_children(|footer| {
                footer
                    .spawn((
                        Node {
                            width: Val::Px(48.0),
                            height: Val::Px(48.0),
                            justify_content: JustifyContent::Center,
                            align_items: AlignItems::Center,
                            padding: UiRect::all(Val::Px(6.0)),
                            ..default()
                        },
                        BackgroundColor(Color::NONE),
                    ))
                    .observe(on_open_settings)
                    .with_children(|btn| {
                        btn.spawn((
                            ImageNode::new(settings_icon),
                            Node {
                                width: Val::Px(36.0),
                                height: Val::Px(36.0),
                                ..default()
                            },
                        ));
                    });
            });
        });
}

fn on_open_settings(
    _trigger: On<PointerClick>,
    mut prev: ResMut<PreviousState>,
    mut next_state: ResMut<NextState<AppState>>,
) {
    prev.0 = AppState::Connecting;
    next_state.set(AppState::Settings);
}

fn despawn_connecting_screen(
    mut commands: Commands,
    query: Query<Entity, With<ConnectingScreenRoot>>,
) {
    for entity in &query {
        commands.entity(entity).despawn();
    }
}

fn update_status_text(
    status: Res<ConnectionStatus>,
    mut query: Query<&mut Text, With<ConnectionStatusText>>,
) {
    if !status.is_changed() {
        return;
    }
    for mut text in &mut query {
        **text = status.display_text();
    }
}
