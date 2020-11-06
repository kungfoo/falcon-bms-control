# Fast Layout Updater

This is a prototype layout manager/updater based on a BSP.

The only way to structure widgets right now it to define splits
on one axis and define percentages along the axis of how to distribute
space.

The overall screen space is partitioned this way and widgets get their
sizes passed down.

Whenever the screen size changes (or device orientation in mobile devices),
the layout is recalculated and widget sizes are updated recursively.

# Elements needed for a widget

- position: x,y
- dimensions: w, h
- function `updateGeometry(x,y,w,h)`

