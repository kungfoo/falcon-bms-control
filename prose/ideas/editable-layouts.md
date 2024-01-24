# Editable Layouts Feature

## What

Provide users with a simple way to build custom screen layouts.

A long press (like 3s or so) anywhere enters layout editing mode. The the pressed on component is selected/highlighted for editing and given an orange border/transparent overlay.

Grid/screen partitions are components as well, but only limited sizing options need to be given at first, like

- 50/50 split vertical/horizontal
- 2x2 grid
- 3x3 grid

Once editing a component, one can select a sub-component from a list. This list includes

- Left MFD
- Right MFD
- ICP and DED
- RWR
- AP and other things button/switch box

## How

Implement gestures

- Long press (3s)
- Click
- Swipe left
- Swipe right

Gestures should be targeted (i.e. provide screen space coordinates of where the gesture was performed), so buttons can be clicked or components can be highlighted in edit mode.

## Questions

- How do we hook up handlers for image data received for components on those screens?
- Are custom layouts serialized and versioned? If so, how can one migrate old layouts?
- How do components present themselves in the selector? Take a screenshot or create a gfx file for each, plus a short title?
