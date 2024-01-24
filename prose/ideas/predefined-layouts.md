# Multiple pre-defined screen layouts feature

## What

Have a collection of useful pre-defined layouts selectable from the settings/preferences/layout selection screen. On that screen/dialog people can select their prefered layout.

- The selected layout should be saved between launches in settings
- Upon launch with no layout stored in settings, use the default layout
- Use the same data structure as custom layouts, just predefined custom layouts so we can reuse the data structure

## How

Implement a serialized lua table structure that defines a bunch of layouts with names and the respective screens and components.

## Layouts to add

- `default`: Current default layout.
- `one-per-screen`: One component per screen
	- Left MFD
	- Right MFD
	- ICP and DED (no RWR to maximize screen space)
	- AP and other switchology panel
- `f15-default`: create a component for data entry in the F15C and use that instead of the ICP

## Questions

- Are predefined layouts .lua files or are they a serialized table with a file name?
- Are layouts versioned? What happens when a new component is added?
- How do we hook up handlers for data received from components on those screens?