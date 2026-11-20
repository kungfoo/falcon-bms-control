use bevy::asset::RenderAssetUsages;
use bevy::prelude::*;
use bevy::render::render_resource::{Extent3d, TextureDimension, TextureFormat};
use bevy::text::{FontSize, FontSource};

use crate::ui::ThemeColors;
use crate::ui::components::fit::FitScale;
use crate::ui::components::{CockpitButton, attach_button_observers};

/// Cached upward-pointing white triangle texture shared by all rocker/dobber switches.
#[derive(Resource)]
pub struct RockerArrowIcon(pub Handle<Image>);

#[derive(Clone, Copy)]
enum ArrowDirection {
    Up,
    Right,
    Down,
    Left,
}

impl ArrowDirection {
    fn rotation_radians(self) -> f32 {
        match self {
            Self::Up => 0.0,
            Self::Right => std::f32::consts::FRAC_PI_2,
            Self::Down => std::f32::consts::PI,
            Self::Left => -std::f32::consts::FRAC_PI_2,
        }
    }
}

/// Generates a 32x32 RGBA8 image of an upward-pointing white isosceles triangle.
pub fn generate_up_triangle_rgba(size: u32) -> Vec<u8> {
    let mut data = vec![0u8; (size * size * 4) as usize];
    let s = size as f32;
    let pad = s * 0.18;
    let top_y = pad;
    let bottom_y = s - pad;
    let height = bottom_y - top_y;
    let half_base = (s - 2.0 * pad) * 0.5;
    let cx = s * 0.5;

    for y in 0..size {
        let fy = y as f32 + 0.5;
        if fy < top_y || fy > bottom_y {
            continue;
        }
        let t = (fy - top_y) / height;
        let half_w = half_base * t;
        for x in 0..size {
            let fx = x as f32 + 0.5;
            let dist = (fx - cx).abs();
            // 1px soft edge for clean anti-aliasing
            let alpha = ((half_w - dist + 0.5).clamp(0.0, 1.0) * 255.0).round() as u8;
            if alpha > 0 {
                let idx = ((y * size + x) * 4) as usize;
                data[idx] = 255;
                data[idx + 1] = 255;
                data[idx + 2] = 255;
                data[idx + 3] = alpha;
            }
        }
    }
    data
}

pub fn init_rocker_arrow_icon(mut commands: Commands, mut images: ResMut<Assets<Image>>) {
    let size = 32;
    let image = Image::new(
        Extent3d {
            width: size,
            height: size,
            depth_or_array_layers: 1,
        },
        TextureDimension::D2,
        generate_up_triangle_rgba(size),
        TextureFormat::Rgba8UnormSrgb,
        RenderAssetUsages::MAIN_WORLD | RenderAssetUsages::RENDER_WORLD,
    );
    commands.insert_resource(RockerArrowIcon(images.add(image)));
}

/// Natural design sizes for portrait and landscape ICP panels (scaled via `FitScale`).
const ICP_PORTRAIT_SIZE: Vec2 = Vec2::new(380.0, 420.0);
const ICP_LANDSCAPE_SIZE: Vec2 = Vec2::new(620.0, 220.0);

struct IcpKeySpec {
    id: &'static str,
    label: Option<&'static str>,
    number: Option<&'static str>,
    round: bool,
}

const TOP_ROW: [IcpKeySpec; 6] = [
    IcpKeySpec {
        id: "COM1",
        label: Some("COM"),
        number: Some("1"),
        round: true,
    },
    IcpKeySpec {
        id: "COM2",
        label: Some("COM"),
        number: Some("2"),
        round: true,
    },
    IcpKeySpec {
        id: "IFF",
        label: Some("IFF"),
        number: None,
        round: true,
    },
    IcpKeySpec {
        id: "LIST",
        label: Some("LIST"),
        number: None,
        round: true,
    },
    IcpKeySpec {
        id: "A-A",
        label: Some("A-A"),
        number: None,
        round: true,
    },
    IcpKeySpec {
        id: "A-G",
        label: Some("A-G"),
        number: None,
        round: true,
    },
];

const PORTRAIT_ROWS: [[IcpKeySpec; 4]; 3] = [
    [
        IcpKeySpec {
            id: "1",
            label: Some("T-ILS"),
            number: Some("1"),
            round: false,
        },
        IcpKeySpec {
            id: "2",
            label: Some("ALOW"),
            number: Some("2"),
            round: false,
        },
        IcpKeySpec {
            id: "3",
            label: None,
            number: Some("3"),
            round: false,
        },
        IcpKeySpec {
            id: "RCL",
            label: Some("RCL"),
            number: None,
            round: false,
        },
    ],
    [
        IcpKeySpec {
            id: "4",
            label: Some("STPT"),
            number: Some("4"),
            round: false,
        },
        IcpKeySpec {
            id: "5",
            label: Some("CRUS"),
            number: Some("5"),
            round: false,
        },
        IcpKeySpec {
            id: "6",
            label: Some("TIME"),
            number: Some("6"),
            round: false,
        },
        IcpKeySpec {
            id: "ENTER",
            label: Some("ENTER"),
            number: None,
            round: false,
        },
    ],
    [
        IcpKeySpec {
            id: "7",
            label: Some("MARK"),
            number: Some("7"),
            round: false,
        },
        IcpKeySpec {
            id: "8",
            label: Some("FIX"),
            number: Some("8"),
            round: false,
        },
        IcpKeySpec {
            id: "9",
            label: Some("A-CAL"),
            number: Some("9"),
            round: false,
        },
        IcpKeySpec {
            id: "0",
            label: Some("M-SEL"),
            number: Some("0"),
            round: false,
        },
    ],
];

const LANDSCAPE_ROWS: [[IcpKeySpec; 6]; 2] = [
    [
        IcpKeySpec {
            id: "1",
            label: Some("T-ILS"),
            number: Some("1"),
            round: false,
        },
        IcpKeySpec {
            id: "2",
            label: Some("ALOW"),
            number: Some("2"),
            round: false,
        },
        IcpKeySpec {
            id: "3",
            label: None,
            number: Some("3"),
            round: false,
        },
        IcpKeySpec {
            id: "4",
            label: Some("STPT"),
            number: Some("4"),
            round: false,
        },
        IcpKeySpec {
            id: "5",
            label: Some("CRUS"),
            number: Some("5"),
            round: false,
        },
        IcpKeySpec {
            id: "RCL",
            label: Some("RCL"),
            number: None,
            round: false,
        },
    ],
    [
        IcpKeySpec {
            id: "6",
            label: Some("TIME"),
            number: Some("6"),
            round: false,
        },
        IcpKeySpec {
            id: "7",
            label: Some("MARK"),
            number: Some("7"),
            round: false,
        },
        IcpKeySpec {
            id: "8",
            label: Some("FIX"),
            number: Some("8"),
            round: false,
        },
        IcpKeySpec {
            id: "9",
            label: Some("A-CAL"),
            number: Some("9"),
            round: false,
        },
        IcpKeySpec {
            id: "0",
            label: Some("M-SEL"),
            number: Some("0"),
            round: false,
        },
        IcpKeySpec {
            id: "ENTER",
            label: Some("ENTER"),
            number: None,
            round: false,
        },
    ],
];

pub fn spawn_icp(
    parent: &mut ChildSpawnerCommands,
    id: &str,
    landscape: bool,
    font: Handle<Font>,
    arrow_icon: Handle<Image>,
) {
    let design_size = if landscape {
        ICP_LANDSCAPE_SIZE
    } else {
        ICP_PORTRAIT_SIZE
    };

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
            center
                .spawn((
                    FitScale {
                        design_size,
                        allow_upscale: true,
                    },
                    UiTransform::default(),
                    Node {
                        position_type: PositionType::Absolute,
                        width: Val::Px(design_size.x),
                        height: Val::Px(design_size.y),
                        flex_shrink: 0.0,
                        padding: UiRect::all(Val::Px(12.0)),
                        flex_direction: FlexDirection::Column,
                        justify_content: JustifyContent::Center,
                        align_items: if landscape {
                            AlignItems::Center
                        } else {
                            AlignItems::FlexStart
                        },
                        row_gap: Val::Px(10.0),
                        border_radius: BorderRadius::all(Val::Px(10.0)),
                        ..default()
                    },
                    BackgroundColor(ThemeColors::PANEL),
                ))
                .with_children(|panel| {
                    if landscape {
                        panel
                            .spawn(Node {
                                flex_direction: FlexDirection::Row,
                                column_gap: Val::Px(14.0),
                                align_items: AlignItems::FlexEnd,
                                ..default()
                            })
                            .with_children(|body| {
                                // Left: Top mode row + 2x6 keypad aligned 1-to-1
                                body.spawn(Node {
                                    flex_direction: FlexDirection::Column,
                                    align_items: AlignItems::FlexStart,
                                    row_gap: Val::Px(10.0),
                                    ..default()
                                })
                                .with_children(|left| {
                                    left.spawn(Node {
                                        flex_direction: FlexDirection::Row,
                                        column_gap: Val::Px(8.0),
                                        ..default()
                                    })
                                    .with_children(|row| {
                                        for key in &TOP_ROW {
                                            spawn_icp_key(row, id, key, font.clone(), 52.0);
                                        }
                                    });

                                    left.spawn(Node {
                                        flex_direction: FlexDirection::Column,
                                        row_gap: Val::Px(8.0),
                                        ..default()
                                    })
                                    .with_children(|grid| {
                                        for r in &LANDSCAPE_ROWS {
                                            grid.spawn(Node {
                                                flex_direction: FlexDirection::Row,
                                                column_gap: Val::Px(8.0),
                                                ..default()
                                            })
                                            .with_children(|row| {
                                                for key in r {
                                                    spawn_icp_key(row, id, key, font.clone(), 52.0);
                                                }
                                            });
                                        }
                                    });
                                });

                                // Middle: WPT rocker
                                spawn_wpt_rocker(body, arrow_icon.clone(), 48.0);

                                // Right: 4-way DED dobber
                                spawn_ded_dobber(body, arrow_icon.clone(), 38.0);
                            });
                    } else {
                        // Portrait: Top row of round mode buttons (left-aligned with keypad)
                        panel
                            .spawn(Node {
                                flex_direction: FlexDirection::Row,
                                column_gap: Val::Px(8.0),
                                justify_content: JustifyContent::FlexStart,
                                ..default()
                            })
                            .with_children(|row| {
                                for key in &TOP_ROW {
                                    spawn_icp_key(row, id, key, font.clone(), 52.0);
                                }
                            });

                        // Portrait: 3x4 keypad (left-aligned)
                        for r in &PORTRAIT_ROWS {
                            panel
                                .spawn(Node {
                                    flex_direction: FlexDirection::Row,
                                    column_gap: Val::Px(8.0),
                                    justify_content: JustifyContent::FlexStart,
                                    ..default()
                                })
                                .with_children(|row| {
                                    for key in r {
                                        spawn_icp_key(row, id, key, font.clone(), 56.0);
                                    }
                                });
                        }

                        // Bottom rockers: WPT + 4-way DED dobber with slight left padding
                        panel
                            .spawn(Node {
                                flex_direction: FlexDirection::Row,
                                column_gap: Val::Px(24.0),
                                justify_content: JustifyContent::FlexStart,
                                align_items: AlignItems::Center,
                                margin: UiRect {
                                    left: Val::Px(12.0),
                                    top: Val::Px(4.0),
                                    ..default()
                                },
                                ..default()
                            })
                            .with_children(|rockers| {
                                spawn_wpt_rocker(rockers, arrow_icon.clone(), 48.0);
                                spawn_ded_dobber(rockers, arrow_icon.clone(), 40.0);
                            });
                    }
                });
        });
}

fn spawn_icp_key(
    parent: &mut ChildSpawnerCommands,
    icp_id: &str,
    spec: &IcpKeySpec,
    font: Handle<Font>,
    size: f32,
) {
    let radius = if spec.round {
        BorderRadius::MAX
    } else {
        BorderRadius::all(Val::Px(6.0))
    };

    let mut entity = parent.spawn((
        CockpitButton::icp(Some(icp_id), spec.id),
        Node {
            width: Val::Px(size),
            height: Val::Px(size),
            border: UiRect::all(Val::Px(1.5)),
            border_radius: radius,
            flex_direction: FlexDirection::Column,
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            ..default()
        },
        BackgroundColor(ThemeColors::BUTTON),
        BorderColor::all(ThemeColors::CYAN_OUTLINE),
    ));
    attach_button_observers(&mut entity);

    entity.with_children(|btn| {
        if let Some(label) = spec.label {
            let label_size = if spec.number.is_some() { 11.0 } else { 13.0 };
            btn.spawn((
                Text::new(label),
                TextFont {
                    font: FontSource::Handle(font.clone()),
                    font_size: FontSize::Px(label_size),
                    ..default()
                },
                TextColor(ThemeColors::TEXT),
            ));
        }
        if let Some(num) = spec.number {
            btn.spawn((
                Text::new(num),
                TextFont {
                    font: FontSource::Handle(font),
                    font_size: FontSize::Px(18.0),
                    ..default()
                },
                TextColor(ThemeColors::TEXT),
            ));
        }
    });
}

fn spawn_wpt_rocker(parent: &mut ChildSpawnerCommands, arrow_icon: Handle<Image>, size: f32) {
    parent
        .spawn(Node {
            flex_direction: FlexDirection::Column,
            row_gap: Val::Px(6.0),
            align_items: AlignItems::Center,
            ..default()
        })
        .with_children(|col| {
            spawn_rocker_button(
                col,
                "icp-wpt-next",
                ArrowDirection::Up,
                arrow_icon.clone(),
                size,
            );
            spawn_rocker_button(
                col,
                "icp-wpt-previous",
                ArrowDirection::Down,
                arrow_icon,
                size,
            );
        });
}

fn spawn_ded_dobber(parent: &mut ChildSpawnerCommands, arrow_icon: Handle<Image>, size: f32) {
    parent
        .spawn(Node {
            flex_direction: FlexDirection::Column,
            row_gap: Val::Px(4.0),
            align_items: AlignItems::Center,
            ..default()
        })
        .with_children(|col| {
            spawn_rocker_button(
                col,
                "icp-ded-up",
                ArrowDirection::Up,
                arrow_icon.clone(),
                size,
            );
            col.spawn(Node {
                flex_direction: FlexDirection::Row,
                column_gap: Val::Px(size + 8.0),
                align_items: AlignItems::Center,
                ..default()
            })
            .with_children(|mid| {
                spawn_rocker_button(
                    mid,
                    "icp-ded-return",
                    ArrowDirection::Left,
                    arrow_icon.clone(),
                    size,
                );
                spawn_rocker_button(
                    mid,
                    "icp-ded-seq",
                    ArrowDirection::Right,
                    arrow_icon.clone(),
                    size,
                );
            });
            spawn_rocker_button(col, "icp-ded-down", ArrowDirection::Down, arrow_icon, size);
        });
}

fn spawn_rocker_button(
    parent: &mut ChildSpawnerCommands,
    button_id: &str,
    direction: ArrowDirection,
    arrow_icon: Handle<Image>,
    size: f32,
) {
    let icon_size = (size * 0.55).round();
    let mut entity = parent.spawn((
        CockpitButton::icp(None, button_id),
        Node {
            width: Val::Px(size),
            height: Val::Px(size),
            border: UiRect::all(Val::Px(1.5)),
            border_radius: BorderRadius::all(Val::Px(6.0)),
            justify_content: JustifyContent::Center,
            align_items: AlignItems::Center,
            ..default()
        },
        BackgroundColor(ThemeColors::BUTTON),
        BorderColor::all(ThemeColors::CYAN_OUTLINE),
    ));
    attach_button_observers(&mut entity);

    entity.with_children(|btn| {
        btn.spawn((
            ImageNode::new(arrow_icon),
            Node {
                width: Val::Px(icon_size),
                height: Val::Px(icon_size),
                ..default()
            },
            UiTransform::from_rotation(Rot2::radians(direction.rotation_radians())),
        ));
    });
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn generated_triangle_is_symmetric_and_non_empty() {
        let size = 32u32;
        let rgba = generate_up_triangle_rgba(size);
        assert_eq!(rgba.len(), (size * size * 4) as usize);

        // Top-left corner should be transparent, lower-center should be opaque white.
        assert_eq!(rgba[3], 0);
        let center_idx = (((size * 2 / 3) * size + (size / 2)) * 4) as usize;
        assert_eq!(rgba[center_idx], 255);
        assert_eq!(rgba[center_idx + 3], 255);

        // Horizontally symmetric
        for y in 0..size {
            for x in 0..(size / 2) {
                let left = ((y * size + x) * 4 + 3) as usize;
                let right = ((y * size + (size - 1 - x)) * 4 + 3) as usize;
                assert_eq!(rgba[left], rgba[right]);
            }
        }
    }
}
