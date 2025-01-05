# Falcon BMS Control

## Where is the user documentation?

Please find the latest user documentation here: https://kungfoo.github.io/falcon-bms-control/

## What is this?

`falcon-bms-control` lets you control Falcon BMS from any touch enabled device (tablet or computer).

Releases for iOS, android and Windows are available for free.

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

- photorealistic graphics including buttons and bezels
- replace more advanced and configurable tools like [Helios](https://github.com/HeliosVirtualCockpit/Helios)

# Development

## How can I help extend this?

Anyone can extend the client code and run the development version in the following ways:

- **Linux, Windows and macOS**: use `love2d` to run the client, install using an appropriate package manager
- **Android**: One can install `love2d` for android [love2d in the Play Store](https://play.google.com/store/apps/details?id=org.love2d.android) and use it to run the assembled package (see client folder for details)

## What can I do?

- Look at issues on github: [Github Issues](https://github.com/kungfoo/falcon-bms-control/issues)
- Look at things in the `prose/ideas` folder.

There may be some overlap between issues and the `ideas` folder, but the folder can also be used as a scratch pad while offline.

## Client Release Builds

### Windows Client

The windows client can be built using the appropriate script and run on a Surface or a similar computer with a touchscreen. The windows client is not code-signed.

### Android and iOS Builds

Clients are built for iOS and android are using seperate repositories that are quite involved and require code-signing and certificates to push new releases to the Apple store and Play store, hence they are currently not open source.

# Development prerequisites

- Any text editor, try `vscode`, `vim` or something fancier

## Client development tools

- `love2d` installed using a package manager of sorts
- `stylua` installed using a package manager or `cargo` to format lua code
- (_Recommended_) Unix or linux or the linux subsystem (on windows) to run any of the scripts
- (_Not required_) An android or windows touch device to test using touch features
- (_Not required_) `cargo` and a rust toolchain to build `boon` to build release packages

## Server development tools

- the 'simple' `lua-server` requires the same tools as the `client`
- `server` requires `msbuild` and `nuget` and (probably) a Windows machine with Visual Studio or respective tools installed.

# Contributions guidelines

- Please contribute changes back to this project via a fork and pull-request.
- Please format your code, there are scripts to do it with consistent settings across platforms.
- Your contributions will be subject to the same license as the overall project is.

