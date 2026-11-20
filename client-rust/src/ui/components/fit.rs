use bevy::prelude::*;

/// Computes a uniform scale factor so a rectangle of `design` fits inside `parent`.
pub fn compute_fit_scale(parent: Vec2, design: Vec2, allow_upscale: bool) -> f32 {
    if parent.x <= 0.0 || parent.y <= 0.0 || design.x <= 0.0 || design.y <= 0.0 {
        return 1.0;
    }
    let scale = (parent.x / design.x).min(parent.y / design.y);
    if allow_upscale { scale } else { scale.min(1.0) }
}

/// Resizes `Node::width` and `Node::height` to `design_size * scale` so the element preserves its
/// aspect ratio and fits cleanly inside its parent container.
#[derive(Component, Debug, Clone, Copy)]
pub struct FitBox {
    pub design_size: Vec2,
    pub allow_upscale: bool,
}

/// Keeps `Node` at `design_size` and applies a uniform `UiTransform` scale so complex button
/// panels (like the ICP) scale down/up smoothly without overflowing or distorting typography.
#[derive(Component, Debug, Clone, Copy)]
pub struct FitScale {
    pub design_size: Vec2,
    pub allow_upscale: bool,
}

pub fn update_fit_boxes(
    parents: Query<&ComputedNode, Without<FitBox>>,
    mut boxes: Query<(&ChildOf, &FitBox, &mut Node)>,
) {
    for (child_of, fit, mut node) in &mut boxes {
        if let Ok(parent) = parents.get(child_of.parent()) {
            let parent_size = parent.size() * parent.inverse_scale_factor();
            if parent_size.x <= 0.0 || parent_size.y <= 0.0 {
                continue;
            }
            let scale = compute_fit_scale(parent_size, fit.design_size, fit.allow_upscale);
            let target_w = Val::Px((fit.design_size.x * scale).floor());
            let target_h = Val::Px((fit.design_size.y * scale).floor());
            if node.width != target_w || node.height != target_h {
                node.width = target_w;
                node.height = target_h;
            }
        }
    }
}

pub fn update_fit_scales(
    parents: Query<&ComputedNode, Without<FitScale>>,
    mut scaled: Query<(&ChildOf, &FitScale, &mut UiTransform)>,
) {
    for (child_of, fit, mut transform) in &mut scaled {
        if let Ok(parent) = parents.get(child_of.parent()) {
            let parent_size = parent.size() * parent.inverse_scale_factor();
            if parent_size.x <= 0.0 || parent_size.y <= 0.0 {
                continue;
            }
            let scale = compute_fit_scale(parent_size, fit.design_size, fit.allow_upscale);
            let target = Vec2::splat(scale);
            if (transform.scale - target).length_squared() > 1e-6 {
                transform.scale = target;
            }
        }
    }
}

#[cfg(test)]
mod tests {
    use super::*;

    #[test]
    fn fits_wide_parent_by_height() {
        let scale = compute_fit_scale(Vec2::new(1000.0, 400.0), Vec2::new(500.0, 500.0), true);
        assert!((scale - 0.8).abs() < 1e-5);
    }

    #[test]
    fn fits_tall_parent_by_width() {
        let scale = compute_fit_scale(Vec2::new(250.0, 800.0), Vec2::new(500.0, 500.0), true);
        assert!((scale - 0.5).abs() < 1e-5);
    }

    #[test]
    fn respects_allow_upscale_flag() {
        let down_only = compute_fit_scale(Vec2::new(1200.0, 900.0), Vec2::new(400.0, 200.0), false);
        assert!((down_only - 1.0).abs() < 1e-5);

        let upscale = compute_fit_scale(Vec2::new(1200.0, 900.0), Vec2::new(400.0, 200.0), true);
        assert!((upscale - 3.0).abs() < 1e-5);
    }

    #[test]
    fn handles_zero_dimensions_safely() {
        assert_eq!(
            compute_fit_scale(Vec2::ZERO, Vec2::new(100.0, 100.0), true),
            1.0
        );
        assert_eq!(
            compute_fit_scale(Vec2::new(100.0, 100.0), Vec2::ZERO, true),
            1.0
        );
    }
}
