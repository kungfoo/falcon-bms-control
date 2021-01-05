# Falcon BMS Control

Falcon BMS Control lets you control Falcon BMS from any touch enabled device.

Releases for both iOS and android are available for free. The client and server code will be open-source once it is polished and documented.

## TL;DR: How to use it?

- Install the app on your device
- Enable RTT exports in `${falcon bms install}/user/config/falcon_bms.cfg`: set `g_bExportRTTTextures 1`
- Allow the server application in Windows Firewall

## iOS app

Find the iOS release of the app here:

[Falcon BMS Control for iOS](https://apps.apple.com/tt/app/falcon-bms-control/id1542670299)


## Android app

The android app is in open testing, available on the play store:

[Falcon BMS Control for Android](https://play.google.com/store/apps/details?id=ch.mollusca.falconbmscontrol)


## Windows server application

The client applications for both iOS and android will try to discover and connect to the `falcon-bms-control` server running on the same network automatically.

Download the server application from here: [Releases](https://github.com/kungfoo/falcon-bms-control/releases/tag/0.1-beta) and run the application alongside Falcon BMS.

If automatic discovery does not work for you on your network, please file an issue at: [Issues](https://github.com/kungfoo/falcon-bms-control/issues)

### Windows Firewall

Windows Firewal will ask you to allow the server application to communicate on the network when you first run it. Not allowing it to do that will render the clients unable to discover the server and unusable.

## FAQ

### I get a windows language switch popup on some keypresses?

On some systems with multiple input languages, the shortcut for language input switching conflicts with the default BMS key binds. The easiest solution is probably to unbind the shortcut in Windows language preferences.

## Client Goals

- be cross platform
- require zero configuration (maybe allow for some)
- auto discovery of server from multiple clients
- responsive controls
- work well on touch devices
- make most of the screen space available

## Client Non-Goals

- photorealistic graphics
- replace more advanced and configurable tools like [Helios](https://github.com/HeliosVirtualCockpit/Helios)

## Privacy Policy

This app collects _none_ of your data. No data whatsoever is collected.
