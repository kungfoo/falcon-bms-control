# Falcon BMS Control

## What is this?

Falcon BMS Control lets you control Falcon BMS from any touch enabled device (tablet or computer).

Releases for both iOS and android are available for free.

This repository contains the following modules/folders:

- `client`: The client code, implemented using lua and the (amazing) `love2d` framework.
- `lua-server`: A fully functioning lua mock server, very useful for development, testing and debugging. Runs anywhere, does not require BMS to be running.
- `server`: The actual server that reads from BMS shared memory and invokes callbacks. Currently in C#.

## Client Goals

- be cross platform
- require zero configuration (maybe allow for some)
- auto discovery of server from multiple clients
- configurable fixed server address to skip discovery
- responsive controls
- work well on touch devices
- make most of the screen space available (as far as possible with simple layouts)

## Client Non-Goals

- photorealistic graphics
- replace more advanced and configurable tools like [Helios](https://github.com/HeliosVirtualCockpit/Helios)

# Development

## How can I help extend this?

Anyone can extend the client code and run the development version in the following ways:

- **Linux, Windows and macOS**: use `love2d` to run the client, install using an appropriate package manager
- **Android**: One can install `love2d` for android [love2d in the Play Store](https://play.google.com/store/apps/details?id=org.love2d.android) and use it to run the assembled package (see client folder for details)

## Client Release Builds

### Windows Client

The windows client can be built using the appropriate script and run on a Surface or a similar computer with a touchscreen. The windows client is not code-signed.

### Android and iOS Builds

Clients are built for iOS and android are using seperate repositories that are quite involved and require code-signing to push new releases to the Apple store and Play store, hence they are currently not open source.

# Development prerequisites

- Any text editor, try `vscode`, `vim` or something fancier.
- `love2d` installed using a package manager of sorts
- `stylua` installed using a package manager or `cargo` to format lua code
- (_Not required_) An android or windows touch device to test using touch features.


