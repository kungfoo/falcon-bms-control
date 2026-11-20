use bevy::prelude::*;

use crate::network::textures::StreamedTextures;
use crate::protocol::TextureChannel;
use crate::ui::ThemeColors;
use crate::ui::components::fit::FitBox;

/// Design dimensions matching the original LÖVE2D RWR (240x240 display + 10px padding each side).
const RWR_DESIGN_SIZE: Vec2 = Vec2::new(260.0, 260.0);

pub fn spawn_rwr(parent: &mut ChildSpawnerCommands, id: &str, textures: &StreamedTextures) {
    let channel = TextureChannel::from_identifier(id).unwrap_or(TextureChannel::Rwr);
    let image_handle = textures.handle(channel);

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
                    FitBox {
                        design_size: RWR_DESIGN_SIZE,
                        allow_upscale: true,
                    },
                    Node {
                        position_type: PositionType::Absolute,
                        width: Val::Px(RWR_DESIGN_SIZE.x),
                        height: Val::Px(RWR_DESIGN_SIZE.y),
                        padding: UiRect::all(Val::Percent(4.0)),
                        border_radius: BorderRadius::all(Val::Px(10.0)),
                        justify_content: JustifyContent::Center,
                        align_items: AlignItems::Center,
                        ..default()
                    },
                    BackgroundColor(ThemeColors::PANEL),
                ))
                .with_children(|bezel| {
                    // Circular clipped RWR display
                    bezel
                        .spawn((
                            Node {
                                width: Val::Percent(100.0),
                                height: Val::Percent(100.0),
                                overflow: Overflow::clip(),
                                border: UiRect::all(Val::Px(1.5)),
                                border_radius: BorderRadius::MAX,
                                ..default()
                            },
                            BackgroundColor(ThemeColors::BLACK),
                            BorderColor::all(ThemeColors::RWR_RETICLE),
                        ))
                        .with_children(|scope| {
                            scope.spawn((
                                ImageNode::new(image_handle),
                                Node {
                                    width: Val::Percent(100.0),
                                    height: Val::Percent(100.0),
                                    ..default()
                                },
                            ));

                            // Inner range ring overlay
                            scope.spawn((
                                Node {
                                    position_type: PositionType::Absolute,
                                    left: Val::Percent(25.0),
                                    top: Val::Percent(25.0),
                                    width: Val::Percent(50.0),
                                    height: Val::Percent(50.0),
                                    border: UiRect::all(Val::Px(1.0)),
                                    border_radius: BorderRadius::MAX,
                                    ..default()
                                },
                                BorderColor::all(ThemeColors::RWR_RETICLE),
                            ));

                            // Center dot
                            scope.spawn((
                                Node {
                                    position_type: PositionType::Absolute,
                                    left: Val::Percent(49.0),
                                    top: Val::Percent(49.0),
                                    width: Val::Percent(2.0),
                                    height: Val::Percent(2.0),
                                    border_radius: BorderRadius::MAX,
                                    ..default()
                                },
                                BackgroundColor(ThemeColors::RWR_RETICLE),
                            ));
                        });
                });
        });
}
