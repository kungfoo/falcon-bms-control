use bevy::prelude::*;
use bevy::text::{FontSize, FontSource};
use bevy::window::PrimaryWindow;

use crate::layouts::LayoutRegistry;
use crate::settings::{Settings, is_valid_ipv4};
use crate::ui::{AppState, PreviousState, ThemeColors};

#[derive(Component)]
struct SettingsScreenRoot;

#[derive(Component)]
struct SettingsColumnsContainer;

#[derive(Component)]
struct SettingsLeftColumn;

#[derive(Component)]
struct SettingsRightColumn;

#[derive(Component)]
struct SettingRow;

#[derive(Component)]
struct SettingsHeaderText;

#[derive(Component)]
struct SettingsSectionHeaderText;

#[derive(Component)]
struct SettingLabelText;

#[derive(Component)]
struct IpButton;

#[derive(Component)]
struct IpNumpadDialog;

#[derive(Component)]
struct IpNumpadPreviewBox;

#[derive(Component)]
struct IpNumpadTitleText;

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
    last_viewport: Vec2,
}

#[derive(Debug, Clone, Copy, PartialEq)]
pub(crate) struct SettingsLayoutMetrics {
    pub two_column: bool,
    pub stack_rows: bool,
    pub root_padding: f32,
    pub root_gap: f32,
    pub section_gap: f32,
    pub row_gap: f32,
    pub header_font: f32,
    pub section_font: f32,
    pub label_font: f32,
    pub control_font: f32,
    pub desc_font: f32,
    pub control_height: f32,
    pub ip_min_width: f32,
    pub rate_btn_width: f32,
    pub step_btn_width: f32,
    pub toggle_btn_width: f32,
    pub layout_pad_x: f32,
    pub layout_pad_y: f32,
    pub numpad_dialog_width: f32,
    pub numpad_padding: f32,
    pub numpad_gap: f32,
    pub numpad_key_height: f32,
    pub numpad_preview_height: f32,
    pub numpad_font: f32,
}

pub(crate) fn compute_settings_layout_metrics(viewport: Vec2) -> SettingsLayoutMetrics {
    let w = viewport.x.max(240.0);
    let h = viewport.y.max(240.0);
    let two_column = w >= 540.0 && h < 600.0;
    let col_width = if two_column {
        (w - 24.0 - 16.0) * 0.5
    } else {
        (w - 24.0).min(760.0)
    };
    let stack_rows = col_width < 430.0;

    let (
        root_padding,
        root_gap,
        section_gap,
        row_gap,
        header_font,
        section_font,
        label_font,
        control_font,
        desc_font,
        control_height,
    ) = if two_column {
        let compact_h = (h / 440.0).clamp(0.72, 1.0);
        let short = h < 420.0;
        (
            if short { 8.0 } else { 14.0 },
            if short { 6.0 } else { 10.0 },
            if short { 4.0 } else { 8.0 },
            if short { 2.0 } else { 4.0 },
            (26.0 * compact_h).round(),
            (22.0 * compact_h).round(),
            (16.0 * compact_h).round(),
            (15.0 * compact_h).round(),
            (14.0 * compact_h).round(),
            (38.0 * compact_h).round(),
        )
    } else {
        let compact_h = (h / 660.0).clamp(0.76, 1.0);
        let compact_w = (w / 420.0).clamp(0.82, 1.0);
        let scale = compact_h.min(compact_w);
        (
            if w < 420.0 || h < 600.0 { 12.0 } else { 20.0 },
            (12.0 * scale).round(),
            (10.0 * scale).round(),
            (4.0 * scale).round(),
            (30.0 * scale).round(),
            (24.0 * scale).round(),
            (18.0 * scale).round(),
            (16.0 * scale).round(),
            (15.0 * scale).round(),
            (40.0 * scale).round(),
        )
    };

    let ip_min_width = (col_width * 0.55).clamp(180.0, 260.0).round();
    let rate_btn_width = if col_width < 290.0 {
        ((col_width - 12.0) / 3.0).floor().max(54.0)
    } else {
        (82.0 * (col_width / 360.0).clamp(0.75, 1.0)).round()
    };
    let step_btn_width = if col_width < 290.0 { 36.0 } else { 44.0 };
    let toggle_btn_width = if col_width < 290.0 { 88.0 } else { 110.0 };
    let layout_pad_x = if col_width < 320.0 || h < 420.0 {
        10.0
    } else {
        14.0
    };
    let layout_pad_y = if h < 420.0 { 6.0 } else { 10.0 };

    let numpad_dialog_width = (w - 24.0).clamp(240.0, 340.0).round();
    let compact_modal = h < 460.0;
    let numpad_padding = if compact_modal { 10.0 } else { 18.0 };
    let numpad_gap = if compact_modal { 6.0 } else { 12.0 };
    let numpad_preview_height = if compact_modal { 36.0 } else { 46.0 };
    let numpad_key_height = ((h - 175.0) / 5.0).clamp(28.0, 48.0).floor();
    let numpad_font = if compact_modal { 15.0 } else { 18.0 };

    SettingsLayoutMetrics {
        two_column,
        stack_rows,
        root_padding,
        root_gap,
        section_gap,
        row_gap,
        header_font,
        section_font,
        label_font,
        control_font,
        desc_font,
        control_height,
        ip_min_width,
        rate_btn_width,
        step_btn_width,
        toggle_btn_width,
        layout_pad_x,
        layout_pad_y,
        numpad_dialog_width,
        numpad_padding,
        numpad_gap,
        numpad_key_height,
        numpad_preview_height,
        numpad_font,
    }
}

pub struct SettingsScreenPlugin;

impl Plugin for SettingsScreenPlugin {
    fn build(&self, app: &mut App) {
        app.init_resource::<SettingsScreenState>()
            .add_systems(OnEnter(AppState::Settings), spawn_settings_screen)
            .add_systems(OnExit(AppState::Settings), despawn_settings_screen)
            .add_systems(
                Update,
                (apply_responsive_settings_layout, sync_settings_ui)
                    .run_if(in_state(AppState::Settings)),
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
    screen_state.last_viewport = Vec2::ZERO;

    let font = asset_server.load("fonts/b612/B612Mono-Regular.ttf");
    let close_icon = asset_server.load("icons/close.png");

    commands
        .spawn((
            SettingsScreenRoot,
            Node {
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                padding: UiRect::all(Val::Px(12.0)),
                flex_direction: FlexDirection::Column,
                justify_content: JustifyContent::SpaceBetween,
                align_items: AlignItems::Center,
                row_gap: Val::Px(10.0),
                overflow: Overflow::clip(),
                ..default()
            },
            BackgroundColor(ThemeColors::BACKGROUND),
        ))
        .with_children(|root| {
            root.spawn((
                SettingsColumnsContainer,
                Node {
                    width: Val::Percent(100.0),
                    max_width: Val::Px(760.0),
                    flex_grow: 1.0,
                    min_width: Val::Px(0.0),
                    min_height: Val::Px(0.0),
                    margin: UiRect::bottom(Val::Px(48.0)),
                    flex_direction: FlexDirection::Column,
                    row_gap: Val::Px(10.0),
                    column_gap: Val::Px(16.0),
                    overflow: Overflow::clip(),
                    ..default()
                },
            ))
            .with_children(|columns| {
                // Left / top column: general settings
                columns
                    .spawn((
                        SettingsLeftColumn,
                        Node {
                            width: Val::Percent(100.0),
                            min_width: Val::Px(0.0),
                            min_height: Val::Px(0.0),
                            flex_direction: FlexDirection::Column,
                            row_gap: Val::Px(10.0),
                            ..default()
                        },
                    ))
                    .with_children(|content| {
                        // Header
                        content.spawn((
                            SettingsHeaderText,
                            Text::new("Settings"),
                            TextFont {
                                font: FontSource::Handle(font.clone()),
                                font_size: FontSize::Px(28.0),
                                ..default()
                            },
                            TextColor(ThemeColors::TEXT),
                        ));

                        // Row 1: Server IP
                        spawn_setting_row(
                            content,
                            "Server IP (empty = auto)",
                            font.clone(),
                            |right| {
                                let ip_label = settings
                                    .ip
                                    .clone()
                                    .unwrap_or_else(|| "[automatic discovery]".to_string());
                                right
                                    .spawn((
                                        IpButton,
                                        Node {
                                            min_width: Val::Px(200.0),
                                            max_width: Val::Percent(100.0),
                                            height: Val::Px(40.0),
                                            padding: UiRect::horizontal(Val::Px(10.0)),
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
                                                font_size: FontSize::Px(16.0),
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
                                    column_gap: Val::Px(6.0),
                                    max_width: Val::Percent(100.0),
                                    ..default()
                                })
                                .with_children(|group| {
                                    for rate in [15u16, 30, 60] {
                                        let active = settings.refresh_rate == rate;
                                        group
                                            .spawn((
                                                RefreshRateOption(rate),
                                                Node {
                                                    width: Val::Px(72.0),
                                                    height: Val::Px(38.0),
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
                                                        font_size: FontSize::Px(15.0),
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
                                            font_size: FontSize::Px(16.0),
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
                                        width: Val::Px(100.0),
                                        height: Val::Px(38.0),
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
                                            font_size: FontSize::Px(15.0),
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
                                        width: Val::Px(100.0),
                                        height: Val::Px(38.0),
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
                                            font_size: FontSize::Px(15.0),
                                            ..default()
                                        },
                                        TextColor(ThemeColors::TEXT),
                                    ));
                                });
                        });
                    });

                // Right / bottom column: screen layout selection
                columns
                    .spawn((
                        SettingsRightColumn,
                        Node {
                            width: Val::Percent(100.0),
                            min_width: Val::Px(0.0),
                            min_height: Val::Px(0.0),
                            flex_direction: FlexDirection::Column,
                            row_gap: Val::Px(10.0),
                            ..default()
                        },
                    ))
                    .with_children(|layout_col| {
                        layout_col.spawn((
                            SettingsSectionHeaderText,
                            Text::new("Screen layout"),
                            TextFont {
                                font: FontSource::Handle(font.clone()),
                                font_size: FontSize::Px(22.0),
                                ..default()
                            },
                            TextColor(ThemeColors::TEXT),
                        ));

                        layout_col
                            .spawn(Node {
                                width: Val::Percent(100.0),
                                flex_direction: FlexDirection::Row,
                                flex_wrap: FlexWrap::Wrap,
                                column_gap: Val::Px(8.0),
                                row_gap: Val::Px(6.0),
                                ..default()
                            })
                            .with_children(|choices| {
                                for layout in registry.all() {
                                    let active = settings.layout == layout.id;
                                    choices
                                        .spawn((
                                            LayoutOptionButton(layout.id.clone()),
                                            Node {
                                                padding: UiRect::axes(Val::Px(12.0), Val::Px(8.0)),
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
                                                    font_size: FontSize::Px(15.0),
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
                        layout_col.spawn((
                            LayoutDescriptionText,
                            Text::new(current_desc),
                            TextFont {
                                font: FontSource::Handle(font.clone()),
                                font_size: FontSize::Px(14.0),
                                ..default()
                            },
                            TextColor(ThemeColors::MUTED_TEXT),
                        ));
                    });
            });

            // Close button pinned to the lower-right corner, matching CockpitScreenRoot's settings button
            root.spawn((
                Node {
                    position_type: PositionType::Absolute,
                    right: Val::Px(10.0),
                    bottom: Val::Px(12.0),
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
        .spawn((
            SettingRow,
            Node {
                width: Val::Percent(100.0),
                min_width: Val::Px(0.0),
                flex_direction: FlexDirection::Row,
                justify_content: JustifyContent::SpaceBetween,
                align_items: AlignItems::Center,
                column_gap: Val::Px(10.0),
                row_gap: Val::Px(4.0),
                ..default()
            },
        ))
        .with_children(|row| {
            row.spawn((
                SettingLabelText,
                Text::new(label),
                TextFont {
                    font: FontSource::Handle(font),
                    font_size: FontSize::Px(16.0),
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
                overflow: Overflow::clip(),
                ..default()
            },
            BackgroundColor(Color::srgba(0.0, 0.0, 0.0, 0.78)),
            GlobalZIndex(10),
        ))
        .with_children(|backdrop| {
            backdrop
                .spawn((
                    IpNumpadDialog,
                    Node {
                        width: Val::Px(340.0),
                        max_height: Val::Percent(94.0),
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
                        IpNumpadTitleText,
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
                            IpNumpadPreviewBox,
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
fn apply_responsive_settings_layout(
    windows: Query<&Window, With<PrimaryWindow>>,
    mut screen_state: ResMut<SettingsScreenState>,
    mut node_sets: ParamSet<(
        ParamSet<(
            Query<&mut Node, With<SettingsScreenRoot>>,
            Query<&mut Node, With<SettingsColumnsContainer>>,
            Query<&mut Node, With<SettingsLeftColumn>>,
            Query<&mut Node, With<SettingsRightColumn>>,
            Query<&mut Node, With<SettingRow>>,
            Query<&mut Node, With<IpButton>>,
            Query<&mut Node, With<RefreshRateOption>>,
            Query<&mut Node, With<QualityStepButton>>,
        )>,
        ParamSet<(
            Query<&mut Node, Or<(With<VibrateToggleButton>, With<FullscreenToggleButton>)>>,
            Query<&mut Node, With<LayoutOptionButton>>,
            Query<&mut Node, With<IpNumpadDialog>>,
            Query<&mut Node, With<IpNumpadPreviewBox>>,
            Query<&mut Node, With<NumpadKey>>,
        )>,
    )>,
    mut fonts: ParamSet<(
        Query<&mut TextFont, With<SettingsHeaderText>>,
        Query<&mut TextFont, With<SettingsSectionHeaderText>>,
        Query<&mut TextFont, With<SettingLabelText>>,
        Query<&mut TextFont, With<LayoutDescriptionText>>,
        Query<
            &mut TextFont,
            Or<(
                With<IpDisplayText>,
                With<QualityValueText>,
                With<VibrateToggleText>,
                With<FullscreenToggleText>,
            )>,
        >,
        Query<&mut TextFont, Or<(With<IpNumpadTitleText>, With<IpNumpadPreviewText>)>>,
        Query<&mut TextFont>,
    )>,
    children_query: Query<&Children>,
    button_parents: Query<
        Entity,
        Or<(
            With<RefreshRateOption>,
            With<QualityStepButton>,
            With<LayoutOptionButton>,
            With<NumpadKey>,
        )>,
    >,
    numpad_keys: Query<Entity, With<NumpadKey>>,
) {
    let Ok(window) = windows.single() else {
        return;
    };
    let viewport = Vec2::new(window.width(), window.height());
    if viewport.x <= 0.0 || viewport.y <= 0.0 || viewport == screen_state.last_viewport {
        return;
    }
    screen_state.last_viewport = viewport;

    let m = compute_settings_layout_metrics(viewport);

    {
        let mut layout_nodes = node_sets.p0();
        for mut node in &mut layout_nodes.p0() {
            node.padding = UiRect::all(Val::Px(m.root_padding));
            node.row_gap = Val::Px(m.root_gap);
        }

        for mut node in &mut layout_nodes.p1() {
            if m.two_column {
                node.flex_direction = FlexDirection::Row;
                node.max_width = Val::Percent(100.0);
                node.align_items = AlignItems::FlexStart;
            } else {
                node.flex_direction = FlexDirection::Column;
                node.max_width = Val::Px(760.0);
                node.align_items = AlignItems::Stretch;
            }
            node.row_gap = Val::Px(m.section_gap);
            node.column_gap = Val::Px(16.0);
        }

        for mut node in &mut layout_nodes.p2() {
            if m.two_column {
                node.width = Val::Auto;
                node.flex_grow = 1.0;
                node.flex_basis = Val::Px(0.0);
            } else {
                node.width = Val::Percent(100.0);
                node.flex_grow = 0.0;
                node.flex_basis = Val::Auto;
            }
            node.row_gap = Val::Px(m.section_gap);
        }

        for mut node in &mut layout_nodes.p3() {
            if m.two_column {
                node.width = Val::Auto;
                node.flex_grow = 1.0;
                node.flex_basis = Val::Px(0.0);
            } else {
                node.width = Val::Percent(100.0);
                node.flex_grow = 0.0;
                node.flex_basis = Val::Auto;
            }
            node.row_gap = Val::Px(m.section_gap);
        }

        for mut node in &mut layout_nodes.p4() {
            if m.stack_rows {
                node.flex_direction = FlexDirection::Column;
                node.justify_content = JustifyContent::FlexStart;
                node.align_items = AlignItems::FlexStart;
            } else {
                node.flex_direction = FlexDirection::Row;
                node.justify_content = JustifyContent::SpaceBetween;
                node.align_items = AlignItems::Center;
            }
            node.row_gap = Val::Px(m.row_gap);
        }

        for mut node in &mut layout_nodes.p5() {
            node.min_width = Val::Px(m.ip_min_width);
            node.height = Val::Px(m.control_height);
        }

        for mut node in &mut layout_nodes.p6() {
            node.width = Val::Px(m.rate_btn_width);
            node.height = Val::Px(m.control_height);
        }

        for mut node in &mut layout_nodes.p7() {
            node.width = Val::Px(m.step_btn_width);
            node.height = Val::Px(m.control_height);
        }
    }

    {
        let mut extra_nodes = node_sets.p1();
        for mut node in &mut extra_nodes.p0() {
            node.width = Val::Px(m.toggle_btn_width);
            node.height = Val::Px(m.control_height);
        }

        for mut node in &mut extra_nodes.p1() {
            node.padding = UiRect::axes(Val::Px(m.layout_pad_x), Val::Px(m.layout_pad_y));
        }

        for mut node in &mut extra_nodes.p2() {
            node.width = Val::Px(m.numpad_dialog_width);
            node.padding = UiRect::all(Val::Px(m.numpad_padding));
            node.row_gap = Val::Px(m.numpad_gap);
        }

        for mut node in &mut extra_nodes.p3() {
            node.height = Val::Px(m.numpad_preview_height);
        }

        for mut node in &mut extra_nodes.p4() {
            node.height = Val::Px(m.numpad_key_height);
        }
    }

    for mut tf in &mut fonts.p0() {
        tf.font_size = FontSize::Px(m.header_font);
    }
    for mut tf in &mut fonts.p1() {
        tf.font_size = FontSize::Px(m.section_font);
    }
    for mut tf in &mut fonts.p2() {
        tf.font_size = FontSize::Px(m.label_font);
    }
    for mut tf in &mut fonts.p3() {
        tf.font_size = FontSize::Px(m.desc_font);
    }
    for mut tf in &mut fonts.p4() {
        tf.font_size = FontSize::Px(m.control_font);
    }
    for mut tf in &mut fonts.p5() {
        tf.font_size = FontSize::Px(m.numpad_font + 2.0);
    }

    let button_font_updates: Vec<(Entity, f32)> = button_parents
        .iter()
        .flat_map(|entity| {
            let size = if numpad_keys.contains(entity) {
                m.numpad_font
            } else {
                m.control_font
            };
            children_query
                .get(entity)
                .into_iter()
                .flat_map(|children| children.iter())
                .map(move |child| (child, size))
        })
        .collect();

    let mut all_fonts = fonts.p6();
    for (child, size) in button_font_updates {
        if let Ok(mut tf) = all_fonts.get_mut(child) {
            tf.font_size = FontSize::Px(size);
        }
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

    #[test]
    fn responsive_metrics_use_two_columns_on_landscape_phones_and_small_tablets() {
        let phone_landscape = compute_settings_layout_metrics(Vec2::new(640.0, 360.0));
        assert!(phone_landscape.two_column);
        assert!(phone_landscape.stack_rows);
        assert!(phone_landscape.numpad_key_height * 5.0 < 360.0);

        let small_tablet_landscape = compute_settings_layout_metrics(Vec2::new(800.0, 480.0));
        assert!(small_tablet_landscape.two_column);
        assert!(small_tablet_landscape.stack_rows);
    }

    #[test]
    fn responsive_metrics_use_single_column_on_portrait_and_desktop() {
        let phone_portrait = compute_settings_layout_metrics(Vec2::new(360.0, 640.0));
        assert!(!phone_portrait.two_column);
        assert!(phone_portrait.stack_rows);
        assert!(phone_portrait.ip_min_width <= 336.0);
        assert!(phone_portrait.rate_btn_width * 3.0 + 12.0 <= 336.0);

        let desktop = compute_settings_layout_metrics(Vec2::new(1920.0, 1080.0));
        assert!(!desktop.two_column);
        assert!(!desktop.stack_rows);
    }

    #[test]
    fn settings_update_systems_run_without_query_conflicts() {
        let mut app = App::new();
        app.add_plugins(MinimalPlugins)
            .add_plugins(bevy::state::app::StatesPlugin)
            .init_state::<AppState>()
            .init_resource::<Settings>()
            .init_resource::<LayoutRegistry>()
            .init_resource::<SettingsScreenState>()
            .add_systems(
                Update,
                (apply_responsive_settings_layout, sync_settings_ui)
                    .run_if(in_state(AppState::Settings)),
            );

        app.world_mut()
            .resource_mut::<NextState<AppState>>()
            .set(AppState::Settings);
        app.update();
        app.update();
    }
}
