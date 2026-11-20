use super::{LayoutDefinition, LayoutNode, ScreenDefinition, SplitDirection};

fn split_x(ratio: f32, margin: f32, first: LayoutNode, second: LayoutNode) -> LayoutNode {
    LayoutNode::Split {
        direction: SplitDirection::X,
        ratio,
        margin,
        first: Box::new(first),
        second: Box::new(second),
    }
}

fn split_y(ratio: f32, margin: f32, first: LayoutNode, second: LayoutNode) -> LayoutNode {
    LayoutNode::Split {
        direction: SplitDirection::Y,
        ratio,
        margin,
        first: Box::new(first),
        second: Box::new(second),
    }
}

fn left_mfd() -> LayoutNode {
    LayoutNode::Mfd {
        id: "f16/left-mfd".into(),
    }
}

fn right_mfd() -> LayoutNode {
    LayoutNode::Mfd {
        id: "f16/right-mfd".into(),
    }
}

fn ded() -> LayoutNode {
    LayoutNode::Ded {
        id: "f16/ded".into(),
    }
}

fn rwr() -> LayoutNode {
    LayoutNode::Rwr {
        id: "f16/rwr".into(),
    }
}

fn icp() -> LayoutNode {
    LayoutNode::Icp {
        id: "f16/icp".into(),
        landscape: false,
    }
}

fn icp_landscape() -> LayoutNode {
    LayoutNode::Icp {
        id: "f16/icp-landscape".into(),
        landscape: true,
    }
}

pub fn builtin_layouts() -> Vec<LayoutDefinition> {
    vec![
        LayoutDefinition {
            id: "default-landscape".into(),
            name: "Default landscape layout".into(),
            description: "The default landscape layout, puts two MFDs side-by-side and the ICP and RWR on another screen.".into(),
            screens: vec![
                ScreenDefinition {
                    name: "Both MFDs".into(),
                    root: split_x(0.5, 20.0, left_mfd(), right_mfd()),
                },
                ScreenDefinition {
                    name: "ICP, DED and RWR".into(),
                    root: split_x(0.3, 20.0, rwr(), split_y(0.3, 10.0, ded(), icp())),
                },
            ],
        },
        LayoutDefinition {
            id: "cockpit-like".into(),
            name: "Cockpit-like layout".into(),
            description: "A layout that mimics a cockpit. Puts two MFDs left and right and a portrait ICP between them. Great for bigger touchscreens.".into(),
            screens: vec![ScreenDefinition {
                name: "Basically all you need.".into(),
                root: split_x(
                    0.33333,
                    20.0,
                    left_mfd(),
                    split_x(0.5, 20.0, split_y(0.33333, 20.0, ded(), icp()), right_mfd()),
                ),
            }],
        },
        LayoutDefinition {
            id: "consolidated".into(),
            name: "All devices consolidated".into(),
            description: "A consolidated layout for bigger screens, puts MFDs and an alternate landscape ICP on the same screen.".into(),
            screens: vec![ScreenDefinition {
                name: "Everything but the kitchen sink.".into(),
                root: split_y(
                    0.35,
                    10.0,
                    split_x(0.65, 10.0, icp_landscape(), ded()),
                    split_x(0.5, 10.0, left_mfd(), right_mfd()),
                ),
            }],
        },
        LayoutDefinition {
            id: "one-device".into(),
            name: "One device per screen".into(),
            description: "Puts each device (each MFD, ICP/DED) on a separate screen. Useful for people with multiple tablets.".into(),
            screens: vec![
                ScreenDefinition {
                    name: "Left MFD".into(),
                    root: left_mfd(),
                },
                ScreenDefinition {
                    name: "Right MFD".into(),
                    root: right_mfd(),
                },
                ScreenDefinition {
                    name: "ICP/DED".into(),
                    root: split_y(0.3, 10.0, ded(), icp()),
                },
            ],
        },
    ]
}
