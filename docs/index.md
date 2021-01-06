# Falcon BMS Control

Falcon BMS Control lets you control Falcon BMS from any touch enabled device.

Releases for both iOS and android are available for free. The client and server code will be open-source once it is polished and documented.

## TL;DR: How to use it?

- Install the app on your device
- Install the windows server application on your BMS host PC: [Releases](https://github.com/kungfoo/falcon-bms-control/releases/)
- Enable RTT exports in `${falcon bms install}/user/config/falcon_bms.cfg`: set `g_bExportRTTTextures 1`
- Allow the server application in Windows Firewall

## iOS app

Find the iOS release of the app here:

[Falcon BMS Control for iOS](https://apps.apple.com/tt/app/falcon-bms-control/id1542670299)


## Android app

The android app is in open testing, available on the play store:

[Falcon BMS Control for Android](https://play.google.com/store/apps/details?id=ch.mollusca.falconbmscontrol)

## Windows client application

A windows client is currently in testing and will be available soon...

## Windows server application

The client applications for both iOS and android will try to discover and connect to the `falcon-bms-control` server running on the same network automatically.

Download the server application from here: [Releases](https://github.com/kungfoo/falcon-bms-control/releases/) and run the application alongside Falcon BMS.

If automatic discovery does not work for you on your network, please file an issue at: [Issues](https://github.com/kungfoo/falcon-bms-control/issues)

### Windows Firewall

Windows Firewal will ask you to allow the server application to communicate on the network when you first run it. Not allowing it to do that will render the clients unable to discover the server and unusable.

## Client settings

All client settings are effective immediately and persised across runs of the application.

- Displays refresh rate: lower for slow devices and networks
  - 15fps
  - 30fps: default
  - 60fps
  
- Displays compression quality: lower for slow networks
  - Range from 50 to 90
  
- Vibration: Provide haptic feedback on button pushes on devices that have the hardware for it.

## FAQ

### I get a windows language switch popup on some keypresses?

On some systems with multiple input languages, the shortcut for language input switching conflicts with the default BMS key binds. The easiest solution is probably to unbind the shortcut in Windows language preferences.

### I have a very slow android device, can I run it?

You will probably want to set the display refresh rate to 15fps on a very slow devices, but the rest of the app should work fine. The changes are effective immediately.

### I am on a very slow wifi, can I save bandwidth?

You can turn down the compression quality of the exported displays to save _significant_ amounts of bandwidth. The default is 80, try setting it to ~60 for a first try. The changes are effective immediately.

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
