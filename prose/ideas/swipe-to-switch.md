# Swipe to switch screens

## What

Swipe left of right changes to the next screen in that direction so one does not have to hunt for the bottom left switcher.

## How

Add gesture support

- Click
- Swipe left
- Swipe right

Each gesture should provide screen space coordinates of where it happened so it can be used to target buttons to be clicked for example.

## Questions

Is there a conflict between certain gestures, such as long-press and click detection.

Currently a click is actually a press followed by a release, but the time in which a release happen is of no importance.

Where do gestures get interpreted in the code and then passed down to whatever has to do something based on an invoked gesture?