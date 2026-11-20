use bevy::prelude::*;

use crate::network::textures::StreamedTextures;
use crate::protocol::TextureChannel;
use crate::ui::ThemeColors;
use crate::ui::components::fit::FitBox;
use crate::ui::components::{CockpitButton, attach_button_observers};

const TOP_OSBS: [&str; 5] = ["OSB_1", "OSB_2", "OSB_3", "OSB_4", "OSB_5"];
const RIGHT_OSBS: [&str; 5] = ["OSB_6", "OSB_7", "OSB_8", "OSB_9", "OSB_10"];
const BOTTOM_OSBS: [&str; 5] = ["OSB_15", "OSB_14", "OSB_13", "OSB_12", "OSB_11"];
const LEFT_OSBS: [&str; 5] = ["OSB_20", "OSB_19", "OSB_18", "OSB_17", "OSB_16"];

/// Proportions matching the original 502px MFD (46px OSB + 410px display + 46px OSB).
const OSB_BAND_PCT: f32 = 46.0 / 502.0 * 100.0;
const DISPLAY_PCT: f32 = 410.0 / 502.0 * 100.0;
/// Exact OSB inter-button gaps from `client/components/mfd.lua` relative to the 410px display:
/// horizontal: (61 - 46) / 410 = 15 / 410; vertical: (58 - 46) / 410 = 12 / 410.
const OSB_HORIZONTAL_GAP_PCT: f32 = 15.0 / 410.0 * 100.0;
const OSB_VERTICAL_GAP_PCT: f32 = 12.0 / 410.0 * 100.0;

pub fn spawn_mfd(parent: &mut ChildSpawnerCommands, id: &str, textures: &StreamedTextures) {
    let channel = TextureChannel::from_identifier(id).unwrap_or(TextureChannel::LeftMfd);
    let image_handle = textures.handle(channel);

    // Outer container centers the strictly-square MFD bezel inside its allocated split slot.
    parent
        .spawn(Node {
            width: Val::Percent(100.0),
            height: Val::Percent(100.0),
            min_width: Val::Px(0.0),
            min_height: Val::Px(0.0),
            overflow: Overflow::clip(),
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            ..default()
        })
        .with_children(|center| {
            // 3x3 CSS Grid guarantees all 4 OSB bands have identical thickness (9.2%)
            // and span the exact width/height of the central display (81.6%).
            center
                .spawn((
                    FitBox {
                        design_size: Vec2::new(502.0, 502.0),
                        allow_upscale: true,
                    },
                    Node {
                        position_type: PositionType::Absolute,
                        width: Val::Px(300.0),
                        height: Val::Px(300.0),
                        display: Display::Grid,
                        grid_template_columns: vec![
                            GridTrack::fr(OSB_BAND_PCT),
                            GridTrack::fr(DISPLAY_PCT),
                            GridTrack::fr(OSB_BAND_PCT),
                        ],
                        grid_template_rows: vec![
                            GridTrack::fr(OSB_BAND_PCT),
                            GridTrack::fr(DISPLAY_PCT),
                            GridTrack::fr(OSB_BAND_PCT),
                        ],
                        padding: UiRect::all(Val::Px(6.0)),
                        column_gap: Val::Px(4.0),
                        row_gap: Val::Px(4.0),
                        border_radius: BorderRadius::all(Val::Px(10.0)),
                        ..default()
                    },
                    BackgroundColor(ThemeColors::PANEL),
                ))
                .with_children(|bezel| {
                    // Row 1, Col 2: Top OSBs (1..5)
                    spawn_horizontal_osb_row(bezel, id, &TOP_OSBS, 1);

                    // Row 2, Col 1: Left OSBs (20..16)
                    spawn_vertical_osb_col(bezel, id, &LEFT_OSBS, 1);

                    // Row 2, Col 2: Live MFD display
                    bezel
                        .spawn((
                            Node {
                                grid_row: GridPlacement::start(2),
                                grid_column: GridPlacement::start(2),
                                width: Val::Percent(100.0),
                                height: Val::Percent(100.0),
                                min_width: Val::Px(0.0),
                                min_height: Val::Px(0.0),
                                overflow: Overflow::clip(),
                                border_radius: BorderRadius::all(Val::Px(4.0)),
                                ..default()
                            },
                            BackgroundColor(ThemeColors::BLACK),
                        ))
                        .with_children(|display| {
                            display.spawn((
                                ImageNode::new(image_handle),
                                Node {
                                    width: Val::Percent(100.0),
                                    height: Val::Percent(100.0),
                                    ..default()
                                },
                            ));
                        });

                    // Row 2, Col 3: Right OSBs (6..10)
                    spawn_vertical_osb_col(bezel, id, &RIGHT_OSBS, 3);

                    // Row 3, Col 2: Bottom OSBs (15..11)
                    spawn_horizontal_osb_row(bezel, id, &BOTTOM_OSBS, 3);
                });
        });
}

fn spawn_horizontal_osb_row(
    parent: &mut ChildSpawnerCommands,
    mfd_id: &str,
    osbs: &[&str; 5],
    grid_row: i16,
) {
    parent
        .spawn(Node {
            grid_row: GridPlacement::start(grid_row),
            grid_column: GridPlacement::start(2),
            width: Val::Percent(100.0),
            height: Val::Percent(100.0),
            min_width: Val::Px(0.0),
            min_height: Val::Px(0.0),
            flex_direction: FlexDirection::Row,
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            column_gap: Val::Percent(OSB_HORIZONTAL_GAP_PCT),
            ..default()
        })
        .with_children(|row| {
            for osb in osbs {
                spawn_osb_button(row, mfd_id, osb, true);
            }
        });
}

fn spawn_vertical_osb_col(
    parent: &mut ChildSpawnerCommands,
    mfd_id: &str,
    osbs: &[&str; 5],
    grid_column: i16,
) {
    parent
        .spawn(Node {
            grid_row: GridPlacement::start(2),
            grid_column: GridPlacement::start(grid_column),
            width: Val::Percent(100.0),
            height: Val::Percent(100.0),
            min_width: Val::Px(0.0),
            min_height: Val::Px(0.0),
            flex_direction: FlexDirection::Column,
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            row_gap: Val::Percent(OSB_VERTICAL_GAP_PCT),
            ..default()
        })
        .with_children(|col| {
            for osb in osbs {
                spawn_osb_button(col, mfd_id, osb, false);
            }
        });
}

fn spawn_osb_button(
    parent: &mut ChildSpawnerCommands,
    mfd_id: &str,
    osb: &str,
    horizontal_band: bool,
) {
    // In a horizontal band, height is the band thickness (9.2% of MFD) and aspect_ratio=1.0
    // makes width identical. In a vertical band, width is the band thickness (9.2% of MFD)
    // and aspect_ratio=1.0 makes height identical.
    let (width, height) = if horizontal_band {
        (Val::Auto, Val::Percent(100.0))
    } else {
        (Val::Percent(100.0), Val::Auto)
    };

    let mut entity = parent.spawn((
        CockpitButton::osb(mfd_id, osb),
        Node {
            width,
            height,
            aspect_ratio: Some(1.0),
            flex_shrink: 0.0,
            border: UiRect::all(Val::Px(1.5)),
            border_radius: BorderRadius::all(Val::Px(5.0)),
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            ..default()
        },
        BackgroundColor(ThemeColors::BUTTON),
        BorderColor::all(ThemeColors::CYAN_OUTLINE),
    ));
    attach_button_observers(&mut entity);
}
