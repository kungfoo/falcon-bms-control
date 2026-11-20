use bevy::prelude::*;
use bevy::text::{FontSize, FontSource};

use crate::layouts::LayoutRegistry;
use crate::settings::{Settings, is_valid_ipv4};
use crate::ui::{AppState, PreviousState, ThemeColors};

#[derive(Component)]
struct SettingsScreenRoot;

#[derive(Component)]
struct IpDisplayText;

#[derive(Component)]
struct IpNumpadModal;

#[derive(Component)]
struct IpNumpadPreviewText;

#[derive(Component)]
struct IpNumpadErrorText;

#[derive(Component)]
struct RefreshRateOption(u16);

#[derive(Component)]
struct QualityValueText;

#[derive(Component)]
struct QualityStepButton(i16);

#[derive(Component)]
struct VibrateToggleButton;

#[derive(Component)]
struct VibrateToggleText;

#[derive(Component)]
struct FullscreenToggleButton;

#[derive(Component)]
struct FullscreenToggleText;

#[derive(Component)]
struct LayoutOptionButton(String);

#[derive(Component)]
struct LayoutDescriptionText;

#[derive(Component)]
struct NumpadKey(NumpadAction);

#[derive(Clone, Copy)]
enum NumpadAction {
    Char(char),
    Backspace,
    Clear,
    Cancel,
    Confirm,
}

/// Editable IPv4 buffer for the touch/mouse-friendly built-in IP numpad.
#[derive(Debug, Clone, Default, PartialEq, Eq)]
pub struct IpNumpadBuffer {
    pub text: String,
    pub error: Option<&'static str>,
}

impl IpNumpadBuffer {
    pub fn new(initial: Option<&str>) -> Self {
        Self {
            text: initial.unwrap_or_default().to_string(),
            error: None,
        }
    }

    pub fn push_char(&mut self, ch: char) {
        if self.text.len() >= 15 {
            return;
        }
        if ch.is_ascii_digit() || ch == '.' {
            self.text.push(ch);
            self.error = None;
        }
    }

    pub fn backspace(&mut self) {
        self.text.pop();
        self.error = None;
    }

    pub fn clear(&mut self) {
        self.text.clear();
        self.error = None;
    }

    /// Returns `Ok(None)` for empty (automatic discovery), `Ok(Some(ip))` for valid IPv4,
    /// or `Err` if malformed.
    pub fn validate(&mut self) -> Result<Option<String>, &'static str> {
        let trimmed = self.text.trim();
        if trimmed.is_empty() {
            self.error = None;
            return Ok(None);
        }
        if is_valid_ipv4(trimmed) {
            self.error = None;
            Ok(Some(trimmed.to_string()))
        } else {
            let msg = "Invalid IPv4 address (e.g. 192.168.1.10)";
            self.error = Some(msg);
            Err(msg)
        }
    }
}

#[derive(Resource, Default)]
struct SettingsScreenState {
    needs_reconnect: bool,
    numpad_open: bool,
    numpad: IpNumpadBuffer,
}

pub struct SettingsScreenPlugin;

impl Plugin for SettingsScreenPlugin {
    fn build(&self, app: &mut App) {
        app.init_resource::<SettingsScreenState>()
            .add_systems(OnEnter(AppState::Settings), spawn_settings_screen)
            .add_systems(OnExit(AppState::Settings), despawn_settings_screen)
            .add_systems(
                Update,
                sync_settings_ui.run_if(in_state(AppState::Settings)),
            );
    }
}

fn spawn_settings_screen(
    mut commands: Commands,
    asset_server: Res<AssetServer>,
    settings: Res<Settings>,
    registry: Res<LayoutRegistry>,
    mut screen_state: ResMut<SettingsScreenState>,
) {
    screen_state.needs_reconnect = false;
    screen_state.numpad_open = false;
    screen_state.numpad = IpNumpadBuffer::new(settings.ip.as_deref());

    let font = asset_server.load("fonts/b612/B612Mono-Regular.ttf");
    let close_icon = asset_server.load("icons/close.png");

    commands
        .spawn((
            SettingsScreenRoot,
            Node {
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                padding: UiRect::all(Val::Px(20.0)),
                flex_direction: FlexDirection::Column,
                justify_content: JustifyContent::SpaceBetween,
                row_gap: Val::Px(14.0),
                ..default()
            },
            BackgroundColor(ThemeColors::BACKGROUND),
        ))
        .with_children(|root| {
            // Scrollable / flexible content column
            root.spawn(Node {
                width: Val::Percent(100.0),
                flex_grow: 1.0,
                flex_direction: FlexDirection::Column,
                row_gap: Val::Px(14.0),
                ..default()
            })
            .with_children(|content| {
                // Header
                content.spawn((
                    Text::new("Settings"),
                    TextFont {
                        font: FontSource::Handle(font.clone()),
                        font_size: FontSize::Px(30.0),
                        ..default()
                    },
                    TextColor(ThemeColors::TEXT),
                ));

                // Row 1: Server IP
                spawn_setting_row(
                    content,
                    "Server IP (empty = auto discovery)",
                    font.clone(),
                    |right| {
                        let ip_label = settings
                            .ip
                            .clone()
                            .unwrap_or_else(|| "[automatic discovery]".to_string());
                        right
                            .spawn((
                                Node {
                                    min_width: Val::Px(260.0),
                                    height: Val::Px(42.0),
                                    padding: UiRect::horizontal(Val::Px(12.0)),
                                    justify_content: JustifyContent::Center,
                                    align_items: AlignItems::Center,
                                    border: UiRect::all(Val::Px(1.5)),
                                    border_radius: BorderRadius::all(Val::Px(6.0)),
                                    ..default()
                                },
                                BackgroundColor(ThemeColors::PANEL),
                                BorderColor::all(ThemeColors::CYAN_OUTLINE),
                            ))
                            .observe(on_open_numpad)
                            .with_children(|btn| {
                                btn.spawn((
                                    IpDisplayText,
                                    Text::new(ip_label),
                                    TextFont {
                                        font: FontSource::Handle(font.clone()),
                                        font_size: FontSize::Px(18.0),
                                        ..default()
                                    },
                                    TextColor(ThemeColors::TEXT),
                                ));
                            });
                    },
                );

                // Row 2: Refresh rate (15 / 30 / 60)
                spawn_setting_row(content, "Display refresh rate", font.clone(), |right| {
                    right
                        .spawn(Node {
                            flex_direction: FlexDirection::Row,
                            column_gap: Val::Px(8.0),
                            ..default()
                        })
                        .with_children(|group| {
                            for rate in [15u16, 30, 60] {
                                let active = settings.refresh_rate == rate;
                                group
                                    .spawn((
                                        RefreshRateOption(rate),
                                        Node {
                                            width: Val::Px(82.0),
                                            height: Val::Px(40.0),
                                            justify_content: JustifyContent::Center,
                                            align_items: AlignItems::Center,
                                            border: UiRect::all(Val::Px(1.5)),
                                            border_radius: BorderRadius::all(Val::Px(6.0)),
                                            ..default()
                                        },
                                        BackgroundColor(if active {
                                            ThemeColors::BUTTON_PRESSED
                                        } else {
                                            ThemeColors::BUTTON
                                        }),
                                        BorderColor::all(ThemeColors::CYAN_OUTLINE),
                                    ))
                                    .observe(on_select_refresh_rate)
                                    .with_children(|btn| {
                                        btn.spawn((
                                            Text::new(format!("{}/s", rate)),
                                            TextFont {
                                                font: FontSource::Handle(font.clone()),
                                                font_size: FontSize::Px(16.0),
                                                ..default()
                                            },
                                            TextColor(ThemeColors::TEXT),
                                        ));
                                    });
                            }
                        });
                });

                // Row 3: Compression quality (50..=90 in steps of 5)
                spawn_setting_row(content, "Compression quality", font.clone(), |right| {
                    right
                        .spawn(Node {
                            flex_direction: FlexDirection::Row,
                            column_gap: Val::Px(10.0),
                            align_items: AlignItems::Center,
                            ..default()
                        })
                        .with_children(|group| {
                            spawn_step_button(group, "-", -5, font.clone());
                            group.spawn((
                                QualityValueText,
                                Text::new(format!("{}", settings.quality)),
                                TextFont {
                                    font: FontSource::Handle(font.clone()),
                                    font_size: FontSize::Px(18.0),
                                    ..default()
                                },
                                TextColor(ThemeColors::TEXT),
                            ));
                            spawn_step_button(group, "+", 5, font.clone());
                        });
                });

                // Row 4: Vibration toggle
                spawn_setting_row(content, "Vibration", font.clone(), |right| {
                    right
                        .spawn((
                            VibrateToggleButton,
                            Node {
                                width: Val::Px(110.0),
                                height: Val::Px(40.0),
                                justify_content: JustifyContent::Center,
                                align_items: AlignItems::Center,
                                border: UiRect::all(Val::Px(1.5)),
                                border_radius: BorderRadius::all(Val::Px(6.0)),
                                ..default()
                            },
                            BackgroundColor(toggle_color(settings.vibrate)),
                            BorderColor::all(ThemeColors::CYAN_OUTLINE),
                        ))
                        .observe(on_toggle_vibrate)
                        .with_children(|btn| {
                            btn.spawn((
                                VibrateToggleText,
                                Text::new(if settings.vibrate { "ON" } else { "OFF" }),
                                TextFont {
                                    font: FontSource::Handle(font.clone()),
                                    font_size: FontSize::Px(16.0),
                                    ..default()
                                },
                                TextColor(ThemeColors::TEXT),
                            ));
                        });
                });

                // Row 5: Fullscreen toggle
                spawn_setting_row(content, "Fullscreen", font.clone(), |right| {
                    right
                        .spawn((
                            FullscreenToggleButton,
                            Node {
                                width: Val::Px(110.0),
                                height: Val::Px(40.0),
                                justify_content: JustifyContent::Center,
                                align_items: AlignItems::Center,
                                border: UiRect::all(Val::Px(1.5)),
                                border_radius: BorderRadius::all(Val::Px(6.0)),
                                ..default()
                            },
                            BackgroundColor(toggle_color(settings.fullscreen)),
                            BorderColor::all(ThemeColors::CYAN_OUTLINE),
                        ))
                        .observe(on_toggle_fullscreen)
                        .with_children(|btn| {
                            btn.spawn((
                                FullscreenToggleText,
                                Text::new(if settings.fullscreen { "ON" } else { "OFF" }),
                                TextFont {
                                    font: FontSource::Handle(font.clone()),
                                    font_size: FontSize::Px(16.0),
                                    ..default()
                                },
                                TextColor(ThemeColors::TEXT),
                            ));
                        });
                });

                // Section: Screen layout
                content.spawn((
                    Text::new("Screen layout"),
                    TextFont {
                        font: FontSource::Handle(font.clone()),
                        font_size: FontSize::Px(24.0),
                        ..default()
                    },
                    TextColor(ThemeColors::TEXT),
                ));

                content
                    .spawn(Node {
                        width: Val::Percent(100.0),
                        flex_direction: FlexDirection::Row,
                        flex_wrap: FlexWrap::Wrap,
                        column_gap: Val::Px(10.0),
                        row_gap: Val::Px(8.0),
                        ..default()
                    })
                    .with_children(|choices| {
                        for layout in registry.all() {
                            let active = settings.layout == layout.id;
                            choices
                                .spawn((
                                    LayoutOptionButton(layout.id.clone()),
                                    Node {
                                        padding: UiRect::axes(Val::Px(14.0), Val::Px(10.0)),
                                        border: UiRect::all(Val::Px(1.5)),
                                        border_radius: BorderRadius::all(Val::Px(6.0)),
                                        ..default()
                                    },
                                    BackgroundColor(if active {
                                        ThemeColors::BUTTON_PRESSED
                                    } else {
                                        ThemeColors::BUTTON
                                    }),
                                    BorderColor::all(ThemeColors::CYAN_OUTLINE),
                                ))
                                .observe(on_select_layout)
                                .with_children(|btn| {
                                    btn.spawn((
                                        Text::new(layout.name.clone()),
                                        TextFont {
                                            font: FontSource::Handle(font.clone()),
                                            font_size: FontSize::Px(16.0),
                                            ..default()
                                        },
                                        TextColor(ThemeColors::TEXT),
                                    ));
                                });
                        }
                    });

                let current_desc = registry
                    .find_or_default(&settings.layout)
                    .description
                    .clone();
                content.spawn((
                    LayoutDescriptionText,
                    Text::new(current_desc),
                    TextFont {
                        font: FontSource::Handle(font.clone()),
                        font_size: FontSize::Px(15.0),
                        ..default()
                    },
                    TextColor(ThemeColors::MUTED_TEXT),
                ));
            });

            // Footer with Close button
            root.spawn(Node {
                width: Val::Percent(100.0),
                height: Val::Px(48.0),
                justify_content: JustifyContent::FlexEnd,
                align_items: AlignItems::Center,
                ..default()
            })
            .with_children(|footer| {
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
                    .observe(on_close_settings)
                    .with_children(|btn| {
                        btn.spawn((
                            ImageNode::new(close_icon),
                            Node {
                                width: Val::Px(34.0),
                                height: Val::Px(34.0),
                                ..default()
                            },
                        ));
                    });
            });

            // Hidden-by-default IP Numpad modal overlay
            spawn_ip_numpad_modal(root, font);
        });
}

fn spawn_setting_row(
    parent: &mut ChildSpawnerCommands,
    label: &str,
    font: Handle<Font>,
    build_control: impl FnOnce(&mut ChildSpawnerCommands),
) {
    parent
        .spawn(Node {
            width: Val::Percent(100.0),
            flex_direction: FlexDirection::Row,
            justify_content: JustifyContent::SpaceBetween,
            align_items: AlignItems::Center,
            column_gap: Val::Px(12.0),
            ..default()
        })
        .with_children(|row| {
            row.spawn((
                Text::new(label),
                TextFont {
                    font: FontSource::Handle(font),
                    font_size: FontSize::Px(18.0),
                    ..default()
                },
                TextColor(ThemeColors::TEXT),
            ));
            build_control(row);
        });
}

fn spawn_step_button(
    parent: &mut ChildSpawnerCommands,
    label: &str,
    delta: i16,
    font: Handle<Font>,
) {
    parent
        .spawn((
            QualityStepButton(delta),
            Node {
                width: Val::Px(44.0),
                height: Val::Px(40.0),
                justify_content: JustifyContent::Center,
                align_items: AlignItems::Center,
                border: UiRect::all(Val::Px(1.5)),
                border_radius: BorderRadius::all(Val::Px(6.0)),
                ..default()
            },
            BackgroundColor(ThemeColors::BUTTON),
            BorderColor::all(ThemeColors::CYAN_OUTLINE),
        ))
        .observe(on_quality_step)
        .with_children(|btn| {
            btn.spawn((
                Text::new(label),
                TextFont {
                    font: FontSource::Handle(font),
                    font_size: FontSize::Px(20.0),
                    ..default()
                },
                TextColor(ThemeColors::TEXT),
            ));
        });
}

fn spawn_ip_numpad_modal(parent: &mut ChildSpawnerCommands, font: Handle<Font>) {
    parent
        .spawn((
            IpNumpadModal,
            Node {
                display: Display::None,
                position_type: PositionType::Absolute,
                left: Val::Px(0.0),
                top: Val::Px(0.0),
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                justify_content: JustifyContent::Center,
                align_items: AlignItems::Center,
                ..default()
            },
            BackgroundColor(Color::srgba(0.0, 0.0, 0.0, 0.78)),
            GlobalZIndex(10),
        ))
        .with_children(|backdrop| {
            backdrop
                .spawn((
                    Node {
                        width: Val::Px(340.0),
                        padding: UiRect::all(Val::Px(18.0)),
                        flex_direction: FlexDirection::Column,
                        row_gap: Val::Px(12.0),
                        border: UiRect::all(Val::Px(2.0)),
                        border_radius: BorderRadius::all(Val::Px(10.0)),
                        ..default()
                    },
                    BackgroundColor(ThemeColors::PANEL),
                    BorderColor::all(ThemeColors::CYAN_OUTLINE),
                ))
                .with_children(|dialog| {
                    dialog.spawn((
                        Text::new("Server IP Address"),
                        TextFont {
                            font: FontSource::Handle(font.clone()),
                            font_size: FontSize::Px(20.0),
                            ..default()
                        },
                        TextColor(ThemeColors::TEXT),
                    ));

                    // Preview box
                    dialog
                        .spawn((
                            Node {
                                width: Val::Percent(100.0),
                                height: Val::Px(46.0),
                                padding: UiRect::horizontal(Val::Px(12.0)),
                                justify_content: JustifyContent::Center,
                                align_items: AlignItems::Center,
                                border_radius: BorderRadius::all(Val::Px(6.0)),
                                ..default()
                            },
                            BackgroundColor(ThemeColors::BLACK),
                        ))
                        .with_children(|box_node| {
                            box_node.spawn((
                                IpNumpadPreviewText,
                                Text::new(""),
                                TextFont {
                                    font: FontSource::Handle(font.clone()),
                                    font_size: FontSize::Px(22.0),
                                    ..default()
                                },
                                TextColor(ThemeColors::ACCENT_GREEN),
                            ));
                        });

                    // Error text
                    dialog.spawn((
                        IpNumpadErrorText,
                        Text::new(""),
                        TextFont {
                            font: FontSource::Handle(font.clone()),
                            font_size: FontSize::Px(13.0),
                            ..default()
                        },
                        TextColor(Color::srgb(0.95, 0.35, 0.35)),
                    ));

                    // Keypad rows
                    let rows: [[(&str, NumpadAction); 3]; 4] = [
                        [
                            ("1", NumpadAction::Char('1')),
                            ("2", NumpadAction::Char('2')),
                            ("3", NumpadAction::Char('3')),
                        ],
                        [
                            ("4", NumpadAction::Char('4')),
                            ("5", NumpadAction::Char('5')),
                            ("6", NumpadAction::Char('6')),
                        ],
                        [
                            ("7", NumpadAction::Char('7')),
                            ("8", NumpadAction::Char('8')),
                            ("9", NumpadAction::Char('9')),
                        ],
                        [
                            (".", NumpadAction::Char('.')),
                            ("0", NumpadAction::Char('0')),
                            ("DEL", NumpadAction::Backspace),
                        ],
                    ];

                    for row in rows {
                        dialog
                            .spawn(Node {
                                width: Val::Percent(100.0),
                                flex_direction: FlexDirection::Row,
                                column_gap: Val::Px(8.0),
                                ..default()
                            })
                            .with_children(|r| {
                                for (label, action) in row {
                                    spawn_numpad_key(r, label, action, font.clone());
                                }
                            });
                    }

                    // Bottom action row: Clear / Cancel / OK
                    dialog
                        .spawn(Node {
                            width: Val::Percent(100.0),
                            flex_direction: FlexDirection::Row,
                            column_gap: Val::Px(8.0),
                            ..default()
                        })
                        .with_children(|r| {
                            spawn_numpad_key(r, "CLR", NumpadAction::Clear, font.clone());
                            spawn_numpad_key(r, "Cancel", NumpadAction::Cancel, font.clone());
                            spawn_numpad_key(r, "OK", NumpadAction::Confirm, font);
                        });
                });
        });
}

fn spawn_numpad_key(
    parent: &mut ChildSpawnerCommands,
    label: &str,
    action: NumpadAction,
    font: Handle<Font>,
) {
    parent
        .spawn((
            NumpadKey(action),
            Node {
                flex_grow: 1.0,
                flex_basis: Val::Px(0.0),
                height: Val::Px(48.0),
                justify_content: JustifyContent::Center,
                align_items: AlignItems::Center,
                border: UiRect::all(Val::Px(1.5)),
                border_radius: BorderRadius::all(Val::Px(6.0)),
                ..default()
            },
            BackgroundColor(ThemeColors::BUTTON),
            BorderColor::all(ThemeColors::CYAN_OUTLINE),
        ))
        .observe(on_numpad_key)
        .with_children(|btn| {
            btn.spawn((
                Text::new(label),
                TextFont {
                    font: FontSource::Handle(font),
                    font_size: FontSize::Px(18.0),
                    ..default()
                },
                TextColor(ThemeColors::TEXT),
            ));
        });
}

fn toggle_color(enabled: bool) -> Color {
    if enabled {
        ThemeColors::BUTTON_PRESSED
    } else {
        ThemeColors::BUTTON
    }
}

fn on_open_numpad(
    _trigger: On<PointerClick>,
    settings: Res<Settings>,
    mut screen_state: ResMut<SettingsScreenState>,
) {
    screen_state.numpad = IpNumpadBuffer::new(settings.ip.as_deref());
    screen_state.numpad_open = true;
}

fn on_numpad_key(
    trigger: On<PointerClick>,
    keys: Query<&NumpadKey>,
    mut settings: ResMut<Settings>,
    mut screen_state: ResMut<SettingsScreenState>,
) {
    let Ok(key) = keys.get(trigger.entity) else {
        return;
    };
    match key.0 {
        NumpadAction::Char(c) => screen_state.numpad.push_char(c),
        NumpadAction::Backspace => screen_state.numpad.backspace(),
        NumpadAction::Clear => screen_state.numpad.clear(),
        NumpadAction::Cancel => {
            screen_state.numpad_open = false;
        }
        NumpadAction::Confirm => {
            if let Ok(validated) = screen_state.numpad.validate() {
                if settings.ip != validated {
                    settings.ip = validated;
                    screen_state.needs_reconnect = true;
                }
                screen_state.numpad_open = false;
            }
        }
    }
}

fn on_select_refresh_rate(
    trigger: On<PointerClick>,
    options: Query<&RefreshRateOption>,
    mut settings: ResMut<Settings>,
) {
    if let Ok(opt) = options.get(trigger.entity) {
        settings.refresh_rate = opt.0;
    }
}

fn on_quality_step(
    trigger: On<PointerClick>,
    steps: Query<&QualityStepButton>,
    mut settings: ResMut<Settings>,
) {
    if let Ok(step) = steps.get(trigger.entity) {
        let next = (settings.quality as i16 + step.0).clamp(50, 90);
        settings.quality = next as u16;
    }
}

fn on_toggle_vibrate(_trigger: On<PointerClick>, mut settings: ResMut<Settings>) {
    settings.vibrate = !settings.vibrate;
}

fn on_toggle_fullscreen(_trigger: On<PointerClick>, mut settings: ResMut<Settings>) {
    settings.fullscreen = !settings.fullscreen;
}

fn on_select_layout(
    trigger: On<PointerClick>,
    options: Query<&LayoutOptionButton>,
    mut settings: ResMut<Settings>,
) {
    if let Ok(opt) = options.get(trigger.entity) {
        settings.layout = opt.0.clone();
    }
}

fn on_close_settings(
    _trigger: On<PointerClick>,
    prev: Res<PreviousState>,
    screen_state: Res<SettingsScreenState>,
    mut next_state: ResMut<NextState<AppState>>,
) {
    if screen_state.needs_reconnect || prev.0 == AppState::Connecting {
        next_state.set(AppState::Connecting);
    } else {
        next_state.set(AppState::Cockpit);
    }
}

#[allow(clippy::too_many_arguments, clippy::type_complexity)]
fn sync_settings_ui(
    settings: Res<Settings>,
    registry: Res<LayoutRegistry>,
    screen_state: Res<SettingsScreenState>,
    mut ip_text: Query<
        &mut Text,
        (
            With<IpDisplayText>,
            Without<QualityValueText>,
            Without<VibrateToggleText>,
            Without<FullscreenToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadPreviewText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut quality_text: Query<
        &mut Text,
        (
            With<QualityValueText>,
            Without<IpDisplayText>,
            Without<VibrateToggleText>,
            Without<FullscreenToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadPreviewText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut vibrate_text: Query<
        &mut Text,
        (
            With<VibrateToggleText>,
            Without<IpDisplayText>,
            Without<QualityValueText>,
            Without<FullscreenToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadPreviewText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut fullscreen_text: Query<
        &mut Text,
        (
            With<FullscreenToggleText>,
            Without<IpDisplayText>,
            Without<QualityValueText>,
            Without<VibrateToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadPreviewText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut layout_desc: Query<
        &mut Text,
        (
            With<LayoutDescriptionText>,
            Without<IpDisplayText>,
            Without<QualityValueText>,
            Without<VibrateToggleText>,
            Without<FullscreenToggleText>,
            Without<IpNumpadPreviewText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut numpad_preview: Query<
        &mut Text,
        (
            With<IpNumpadPreviewText>,
            Without<IpDisplayText>,
            Without<QualityValueText>,
            Without<VibrateToggleText>,
            Without<FullscreenToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadErrorText>,
        ),
    >,
    mut numpad_error: Query<
        &mut Text,
        (
            With<IpNumpadErrorText>,
            Without<IpDisplayText>,
            Without<QualityValueText>,
            Without<VibrateToggleText>,
            Without<FullscreenToggleText>,
            Without<LayoutDescriptionText>,
            Without<IpNumpadPreviewText>,
        ),
    >,
    mut rate_buttons: Query<
        (&RefreshRateOption, &mut BackgroundColor),
        (
            Without<VibrateToggleButton>,
            Without<FullscreenToggleButton>,
            Without<LayoutOptionButton>,
        ),
    >,
    mut vibrate_btn: Query<
        &mut BackgroundColor,
        (
            With<VibrateToggleButton>,
            Without<RefreshRateOption>,
            Without<FullscreenToggleButton>,
            Without<LayoutOptionButton>,
        ),
    >,
    mut fullscreen_btn: Query<
        &mut BackgroundColor,
        (
            With<FullscreenToggleButton>,
            Without<RefreshRateOption>,
            Without<VibrateToggleButton>,
            Without<LayoutOptionButton>,
        ),
    >,
    mut layout_btns: Query<
        (&LayoutOptionButton, &mut BackgroundColor),
        (
            Without<RefreshRateOption>,
            Without<VibrateToggleButton>,
            Without<FullscreenToggleButton>,
        ),
    >,
    mut modal: Query<&mut Node, With<IpNumpadModal>>,
) {
    if settings.is_changed() {
        for mut t in &mut ip_text {
            **t = settings
                .ip
                .clone()
                .unwrap_or_else(|| "[automatic discovery]".to_string());
        }
        for mut t in &mut quality_text {
            **t = format!("{}", settings.quality);
        }
        for mut t in &mut vibrate_text {
            **t = if settings.vibrate { "ON" } else { "OFF" }.to_string();
        }
        for mut t in &mut fullscreen_text {
            **t = if settings.fullscreen { "ON" } else { "OFF" }.to_string();
        }
        let desc = registry
            .find_or_default(&settings.layout)
            .description
            .clone();
        for mut t in &mut layout_desc {
            **t = desc.clone();
        }
        for (opt, mut bg) in &mut rate_buttons {
            *bg = BackgroundColor(toggle_color(settings.refresh_rate == opt.0));
        }
        for mut bg in &mut vibrate_btn {
            *bg = BackgroundColor(toggle_color(settings.vibrate));
        }
        for mut bg in &mut fullscreen_btn {
            *bg = BackgroundColor(toggle_color(settings.fullscreen));
        }
        for (opt, mut bg) in &mut layout_btns {
            *bg = BackgroundColor(toggle_color(settings.layout == opt.0));
        }
    }

    if screen_state.is_changed() {
        for mut node in &mut modal {
            node.display = if screen_state.numpad_open {
                Display::Flex
            } else {
                Display::None
            };
        }
        for mut t in &mut numpad_preview {
            **t = if screen_state.numpad.text.is_empty() {
                "[automatic discovery]".to_string()
            } else {
                screen_state.numpad.text.clone()
            };
        }
        for mut t in &mut numpad_error {
            **t = screen_state.numpad.error.unwrap_or_default().to_string();
        }
    }
}

fn despawn_settings_screen(mut commands: Commands, query: Query<Entity, With<SettingsScreenRoot>>) {
    for entity in &query {
        commands.entity(entity).despawn();
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn numpad_buffer_edits_and_validates_ipv4() {
        let mut buf = IpNumpadBuffer::new(None);
        assert_eq!(buf.validate(), Ok(None));

        for c in "192.168.1.50".chars() {
            buf.push_char(c);
        }
        assert_eq!(buf.text, "192.168.1.50");
        assert_eq!(buf.validate(), Ok(Some("192.168.1.50".to_string())));

        buf.backspace();
        assert_eq!(buf.text, "192.168.1.5");
        assert_eq!(buf.validate(), Ok(Some("192.168.1.5".to_string())));

        buf.clear();
        assert_eq!(buf.validate(), Ok(None));
    }

    #[test]
    fn numpad_buffer_rejects_invalid_ip_and_ignores_non_ip_chars() {
        let mut buf = IpNumpadBuffer::new(None);
        buf.push_char('a');
        assert_eq!(buf.text, "");

        for c in "999.1.2.3".chars() {
            buf.push_char(c);
        }
        assert!(buf.validate().is_err());
        assert!(buf.error.is_some());

        buf.backspace();
        assert!(buf.error.is_none());
    }
}
