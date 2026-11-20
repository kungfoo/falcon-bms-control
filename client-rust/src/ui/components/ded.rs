use bevy::prelude::*;

use crate::network::textures::StreamedTextures;
use crate::protocol::TextureChannel;
use crate::ui::ThemeColors;
use crate::ui::components::fit::FitBox;

/// Design dimensions matching the DED display aspect ratio (400x150 + thin bezel).
const DED_DESIGN_SIZE: Vec2 = Vec2::new(416.0, 166.0);

pub fn spawn_ded(parent: &mut ChildSpawnerCommands, id: &str, textures: &StreamedTextures) {
    let channel = TextureChannel::from_identifier(id).unwrap_or(TextureChannel::Ded);
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
                        design_size: DED_DESIGN_SIZE,
                        allow_upscale: true,
                    },
                    Node {
                        position_type: PositionType::Absolute,
                        width: Val::Px(DED_DESIGN_SIZE.x),
                        height: Val::Px(DED_DESIGN_SIZE.y),
                        padding: UiRect::all(Val::Px(8.0)),
                        border_radius: BorderRadius::all(Val::Px(8.0)),
                        ..default()
                    },
                    BackgroundColor(ThemeColors::PANEL),
                ))
                .with_children(|bezel| {
                    bezel
                        .spawn((
                            Node {
                                width: Val::Percent(100.0),
                                height: Val::Percent(100.0),
                                overflow: Overflow::clip(),
                                border_radius: BorderRadius::all(Val::Px(5.0)),
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
                });
        });
}
