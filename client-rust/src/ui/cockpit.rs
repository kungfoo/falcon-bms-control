use bevy::prelude::*;
use bevy::text::{FontSize, FontSource};

use crate::layouts::{LayoutNode, LayoutRegistry, SplitDirection};
use crate::network::NetworkClient;
use crate::network::textures::StreamedTextures;
use crate::settings::Settings;
use crate::ui::components::icp::RockerArrowIcon;
use crate::ui::components::{ded, fit, icp, load_button_sounds, mfd, rwr};
use crate::ui::{AppState, PreviousState, ThemeColors};

#[derive(Component)]
struct CockpitScreenRoot;

/// Tracks which screen index within the active layout is currently shown, plus which streams are active.
#[derive(Resource, Default)]
pub struct CockpitState {
    pub screen_index: usize,
    pub active_streams: Vec<String>,
    pub rebuild_requested: bool,
}

impl CockpitState {
    pub fn next_screen(&mut self, total_screens: usize) {
        if total_screens > 1 {
            self.screen_index = (self.screen_index + 1) % total_screens;
            self.rebuild_requested = true;
        }
    }

    pub fn clamp_screen_index(&mut self, total_screens: usize) {
        if total_screens == 0 {
            self.screen_index = 0;
        } else if self.screen_index >= total_screens {
            self.screen_index = 0;
        }
    }
}

pub struct CockpitScreenPlugin;

impl Plugin for CockpitScreenPlugin {
    fn build(&self, app: &mut App) {
        app.init_resource::<CockpitState>()
            .add_systems(Startup, (load_button_sounds, icp::init_rocker_arrow_icon))
            .add_systems(OnEnter(AppState::Cockpit), enter_cockpit)
            .add_systems(OnExit(AppState::Cockpit), exit_cockpit)
            .add_systems(
                Update,
                (
                    rebuild_cockpit_if_needed,
                    fit::update_fit_boxes,
                    fit::update_fit_scales,
                )
                    .run_if(in_state(AppState::Cockpit)),
            );
    }
}

fn enter_cockpit(
    mut commands: Commands,
    asset_server: Res<AssetServer>,
    settings: Res<Settings>,
    registry: Res<LayoutRegistry>,
    textures: Res<StreamedTextures>,
    arrow_icon: Res<RockerArrowIcon>,
    client: Res<NetworkClient>,
    mut cockpit: ResMut<CockpitState>,
) {
    cockpit.rebuild_requested = false;
    spawn_active_cockpit_screen(
        &mut commands,
        &asset_server,
        &settings,
        &registry,
        &textures,
        arrow_icon.0.clone(),
        &client,
        &mut cockpit,
    );
}

fn exit_cockpit(
    mut commands: Commands,
    query: Query<Entity, With<CockpitScreenRoot>>,
    client: Res<NetworkClient>,
    mut cockpit: ResMut<CockpitState>,
) {
    stop_active_streams(&client, &mut cockpit);
    for entity in &query {
        commands.entity(entity).despawn();
    }
}

fn rebuild_cockpit_if_needed(
    mut commands: Commands,
    query: Query<Entity, With<CockpitScreenRoot>>,
    asset_server: Res<AssetServer>,
    settings: Res<Settings>,
    registry: Res<LayoutRegistry>,
    textures: Res<StreamedTextures>,
    arrow_icon: Res<RockerArrowIcon>,
    client: Res<NetworkClient>,
    mut cockpit: ResMut<CockpitState>,
) {
    if !cockpit.rebuild_requested {
        return;
    }
    cockpit.rebuild_requested = false;

    stop_active_streams(&client, &mut cockpit);
    for entity in &query {
        commands.entity(entity).despawn();
    }

    spawn_active_cockpit_screen(
        &mut commands,
        &asset_server,
        &settings,
        &registry,
        &textures,
        arrow_icon.0.clone(),
        &client,
        &mut cockpit,
    );
}

fn stop_active_streams(client: &NetworkClient, cockpit: &mut CockpitState) {
    for id in cockpit.active_streams.drain(..) {
        client.stop_stream(id);
    }
}

#[allow(clippy::too_many_arguments)]
fn spawn_active_cockpit_screen(
    commands: &mut Commands,
    asset_server: &AssetServer,
    settings: &Settings,
    registry: &LayoutRegistry,
    textures: &StreamedTextures,
    arrow_icon: Handle<Image>,
    client: &NetworkClient,
    cockpit: &mut CockpitState,
) {
    let layout = registry.find_or_default(&settings.layout);
    let total_screens = layout.screens.len();
    cockpit.clamp_screen_index(total_screens);

    let Some(screen) = layout.screens.get(cockpit.screen_index) else {
        return;
    };

    // Start only the streamed textures visible on this screen.
    let needed_streams = screen.streamed_identifiers();
    for id in &needed_streams {
        client.start_stream(id.clone(), settings.refresh_rate, settings.quality);
    }
    cockpit.active_streams = needed_streams;

    let font = asset_server.load("fonts/b612/B612Mono-Regular.ttf");
    let settings_icon = asset_server.load("icons/settings.png");
    let current_index = cockpit.screen_index;
    let screen_name = screen.name.clone();

    commands
        .spawn((
            CockpitScreenRoot,
            Node {
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                padding: UiRect::all(Val::Px(10.0)),
                flex_direction: FlexDirection::Column,
                row_gap: Val::Px(10.0),
                ..default()
            },
            BackgroundColor(ThemeColors::BACKGROUND),
        ))
        .with_children(|root| {
            // Main cockpit area (95% height via flex_grow)
            root.spawn(Node {
                width: Val::Percent(100.0),
                flex_grow: 1.0,
                min_width: Val::Px(0.0),
                min_height: Val::Px(0.0),
                overflow: Overflow::clip(),
                ..default()
            })
            .with_children(|main_area| {
                spawn_layout_node(main_area, &screen.root, textures, font.clone(), arrow_icon);
            });

            // Footer bar: screen switcher (left/center) + settings icon (right)
            root.spawn(Node {
                width: Val::Percent(100.0),
                height: Val::Px(48.0),
                flex_direction: FlexDirection::Row,
                justify_content: JustifyContent::SpaceBetween,
                align_items: AlignItems::Center,
                ..default()
            })
            .with_children(|footer| {
                if total_screens > 1 {
                    footer
                        .spawn((
                            Node {
                                height: Val::Px(42.0),
                                padding: UiRect::horizontal(Val::Px(14.0)),
                                flex_direction: FlexDirection::Row,
                                align_items: AlignItems::Center,
                                column_gap: Val::Px(10.0),
                                border: UiRect::all(Val::Px(1.5)),
                                border_radius: BorderRadius::all(Val::Px(6.0)),
                                ..default()
                            },
                            BackgroundColor(ThemeColors::PANEL),
                            BorderColor::all(ThemeColors::TEXT),
                        ))
                        .observe(on_switch_screen)
                        .with_children(|switcher| {
                            // Segment indicators
                            for i in 0..total_screens {
                                let color = if i == current_index {
                                    ThemeColors::ACCENT_GREEN
                                } else {
                                    ThemeColors::BUTTON
                                };
                                switcher.spawn((
                                    Node {
                                        width: Val::Px(26.0),
                                        height: Val::Px(16.0),
                                        border_radius: BorderRadius::all(Val::Px(3.0)),
                                        ..default()
                                    },
                                    BackgroundColor(color),
                                ));
                            }
                            switcher.spawn((
                                Text::new(screen_name),
                                TextFont {
                                    font: FontSource::Handle(font.clone()),
                                    font_size: FontSize::Px(14.0),
                                    ..default()
                                },
                                TextColor(ThemeColors::MUTED_TEXT),
                            ));
                        });
                } else {
                    footer.spawn(Node::default());
                }

                // Settings button
                footer
                    .spawn((
                        Node {
                            width: Val::Px(44.0),
                            height: Val::Px(44.0),
                            justify_content: JustifyContent::Center,
                            align_items: AlignItems::Center,
                            ..default()
                        },
                        BackgroundColor(Color::NONE),
                    ))
                    .observe(on_open_settings)
                    .with_children(|btn| {
                        btn.spawn((
                            ImageNode::new(settings_icon),
                            Node {
                                width: Val::Px(34.0),
                                height: Val::Px(34.0),
                                ..default()
                            },
                        ));
                    });
            });
        });
}

fn spawn_layout_node(
    parent: &mut ChildSpawnerCommands,
    node: &LayoutNode,
    textures: &StreamedTextures,
    font: Handle<Font>,
    arrow_icon: Handle<Image>,
) {
    match node {
        LayoutNode::Split {
            direction,
            ratio,
            margin,
            first,
            second,
        } => {
            let ratio = ratio.clamp(0.05, 0.95);
            let flex_direction = match direction {
                SplitDirection::X => FlexDirection::Row,
                SplitDirection::Y => FlexDirection::Column,
            };

            parent
                .spawn(Node {
                    width: Val::Percent(100.0),
                    height: Val::Percent(100.0),
                    min_width: Val::Px(0.0),
                    min_height: Val::Px(0.0),
                    flex_direction,
                    column_gap: Val::Px(*margin),
                    row_gap: Val::Px(*margin),
                    ..default()
                })
                .with_children(|split| {
                    split
                        .spawn(Node {
                            flex_grow: ratio,
                            flex_basis: Val::Px(0.0),
                            min_width: Val::Px(0.0),
                            min_height: Val::Px(0.0),
                            ..default()
                        })
                        .with_children(|slot| {
                            spawn_layout_node(
                                slot,
                                first,
                                textures,
                                font.clone(),
                                arrow_icon.clone(),
                            );
                        });

                    split
                        .spawn(Node {
                            flex_grow: 1.0 - ratio,
                            flex_basis: Val::Px(0.0),
                            min_width: Val::Px(0.0),
                            min_height: Val::Px(0.0),
                            ..default()
                        })
                        .with_children(|slot| {
                            spawn_layout_node(slot, second, textures, font, arrow_icon);
                        });
                });
        }
        LayoutNode::Mfd { id } => {
            mfd::spawn_mfd(parent, id, textures);
        }
        LayoutNode::Ded { id } => {
            ded::spawn_ded(parent, id, textures);
        }
        LayoutNode::Rwr { id } => {
            rwr::spawn_rwr(parent, id, textures);
        }
        LayoutNode::Icp { id, landscape } => {
            icp::spawn_icp(parent, id, *landscape, font, arrow_icon);
        }
    }
}

fn on_switch_screen(
    _trigger: On<PointerClick>,
    settings: Res<Settings>,
    registry: Res<LayoutRegistry>,
    mut cockpit: ResMut<CockpitState>,
) {
    let total = registry.find_or_default(&settings.layout).screens.len();
    cockpit.next_screen(total);
}

fn on_open_settings(
    _trigger: On<PointerClick>,
    mut prev: ResMut<PreviousState>,
    mut next_state: ResMut<NextState<AppState>>,
) {
    prev.0 = AppState::Cockpit;
    next_state.set(AppState::Settings);
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn cockpit_state_cycles_and_clamps_screen_index() {
        let mut state = CockpitState::default();
        assert_eq!(state.screen_index, 0);

        state.next_screen(3);
        assert_eq!(state.screen_index, 1);
        assert!(state.rebuild_requested);

        state.next_screen(3);
        assert_eq!(state.screen_index, 2);

        state.next_screen(3);
        assert_eq!(state.screen_index, 0);

        state.screen_index = 5;
        state.clamp_screen_index(2);
        assert_eq!(state.screen_index, 0);
    }
}
