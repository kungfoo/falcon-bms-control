# Falcon BMS Control Client Code

## Interesting folders

- `client/`: main application startup file
- `client/components`: most of the things that you can interact with on screen

## Less interesting folders

- `lib`: A bunch of supporting libraries and files. Not all the files/functions are currently used.

## How does this all work?

1. The client presents a bunch of components (such as an MFD) on a bunch of screens.
2. Components that want to render shared texture memory contents request them from the server when they become visible. They tell the server to stop sending data when they become invisible, such as not to burden the network with data that is never rendered.
3. When the users clicks something that is relevant to BMS, the client sends an event to the server.
4. The server invokes the defined callback from the currently active keyfile by simulating keyboard presses. 

The server will send a chunk of shared texture memory data as a compressed image with the requested frame rate, iff the contents have changed since the last time the chunk was sent to the client. Image data is sent using an eNet unreliable channel, so packets can be dropped.

The client will present at 60fps regardless of the frame rate of BMS or the updates over the network.

## Why is there no button/switch box for AP and other things?

There is a branch that adds Master Arm, Laser Arm and AP mode switches, but they are impossible to sync with BMS since there is (currently) no way to figure out the current state of the switch in BMS (as far as I know).

## Should I touch the networked discovery code?

Probably not.

## I touched the discovery code and now things don't work!

I hate to say it, but I told you so.