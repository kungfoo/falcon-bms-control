pub mod builtin;

use bevy::prelude::*;
use serde::{Deserialize, Serialize};

use crate::protocol::TextureChannel;

#[derive(Debug, Clone, Copy, PartialEq, Eq, Serialize, Deserialize)]
#[serde(rename_all = "lowercase")]
pub enum SplitDirection {
    X,
    Y,
}

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
#[serde(tag = "type", rename_all = "kebab-case")]
pub enum LayoutNode {
    Split {
        direction: SplitDirection,
        #[serde(default = "default_ratio")]
        ratio: f32,
        #[serde(default = "default_margin")]
        margin: f32,
        first: Box<LayoutNode>,
        second: Box<LayoutNode>,
    },
    Mfd {
        id: String,
    },
    Ded {
        id: String,
    },
    Rwr {
        id: String,
    },
    Icp {
        id: String,
        #[serde(default)]
        landscape: bool,
    },
}

fn default_ratio() -> f32 {
    0.5
}

fn default_margin() -> f32 {
    10.0
}

impl LayoutNode {
    /// Collect all streamed texture identifiers referenced in this subtree.
    pub fn collect_streamed_identifiers(&self, out: &mut Vec<String>) {
        match self {
            LayoutNode::Split { first, second, .. } => {
                first.collect_streamed_identifiers(out);
                second.collect_streamed_identifiers(out);
            }
            LayoutNode::Mfd { id } | LayoutNode::Ded { id } | LayoutNode::Rwr { id } => {
                if TextureChannel::from_identifier(id).is_some() && !out.contains(id) {
                    out.push(id.clone());
                }
            }
            LayoutNode::Icp { .. } => {}
        }
    }
}

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct ScreenDefinition {
    pub name: String,
    pub root: LayoutNode,
}

impl ScreenDefinition {
    pub fn streamed_identifiers(&self) -> Vec<String> {
        let mut ids = Vec::new();
        self.root.collect_streamed_identifiers(&mut ids);
        ids
    }
}

#[derive(Debug, Clone, PartialEq, Serialize, Deserialize)]
pub struct LayoutDefinition {
    pub id: String,
    pub name: String,
    pub description: String,
    pub screens: Vec<ScreenDefinition>,
}

/// Registry of available layouts (starts with built-ins; ready to accept server-provided layouts).
#[derive(Resource, Debug, Clone)]
pub struct LayoutRegistry {
    layouts: Vec<LayoutDefinition>,
}

impl Default for LayoutRegistry {
    fn default() -> Self {
        Self {
            layouts: builtin::builtin_layouts(),
        }
    }
}

impl LayoutRegistry {
    pub fn all(&self) -> &[LayoutDefinition] {
        &self.layouts
    }

    pub fn find(&self, id: &str) -> Option<&LayoutDefinition> {
        self.layouts.iter().find(|l| l.id == id)
    }

    pub fn find_or_default(&self, id: &str) -> &LayoutDefinition {
        self.find(id)
            .or_else(|| self.find("default-landscape"))
            .unwrap_or(&self.layouts[0])
    }

    /// Insert or replace a layout (useful when loading layouts from the server later).
    pub fn upsert(&mut self, layout: LayoutDefinition) {
        if let Some(existing) = self.layouts.iter_mut().find(|l| l.id == layout.id) {
            *existing = layout;
        } else {
            self.layouts.push(layout);
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn builtin_registry_contains_all_four_layouts() {
        let registry = LayoutRegistry::default();
        assert_eq!(registry.all().len(), 4);
        assert!(registry.find("default-landscape").is_some());
        assert!(registry.find("cockpit-like").is_some());
        assert!(registry.find("consolidated").is_some());
        assert!(registry.find("one-device").is_some());
    }

    #[test]
    fn unknown_layout_id_falls_back_to_default_landscape() {
        let registry = LayoutRegistry::default();
        let layout = registry.find_or_default("non-existent");
        assert_eq!(layout.id, "default-landscape");
    }

    #[test]
    fn default_landscape_streamed_identifiers_per_screen() {
        let registry = LayoutRegistry::default();
        let layout = registry.find("default-landscape").unwrap();
        assert_eq!(layout.screens.len(), 2);

        let screen0 = layout.screens[0].streamed_identifiers();
        assert_eq!(screen0, vec!["f16/left-mfd", "f16/right-mfd"]);

        let screen1 = layout.screens[1].streamed_identifiers();
        assert_eq!(screen1, vec!["f16/rwr", "f16/ded"]);
    }

    #[test]
    fn layout_definitions_roundtrip_via_toml_and_msgpack() {
        let registry = LayoutRegistry::default();
        for layout in registry.all() {
            let toml_str = toml::to_string(layout).expect("toml serialize");
            let from_toml: LayoutDefinition = toml::from_str(&toml_str).expect("toml deserialize");
            assert_eq!(&from_toml, layout);

            let mp = rmp_serde::to_vec_named(layout).expect("msgpack serialize");
            let from_mp: LayoutDefinition =
                rmp_serde::from_slice(&mp).expect("msgpack deserialize");
            assert_eq!(&from_mp, layout);
        }
    }
}
