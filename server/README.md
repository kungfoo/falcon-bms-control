# Falcon BMS Universal MFD/ICP Server

## Dependencies

Uses the following tools to build:

- nuget: https://www.nuget.org/downloads, place in `${project-root}/server/`
- msbuild: https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=vs-2019

Install them using their respective installers.

## How to build

Given the tools are properly installed, building a release build should be as eas as:

```
build
```

Building a debug release is:

```
build-debug
```

## Changing code

To change code, it is probably easiest to use either Visual Studio (Community Edititon is enough) or Visual Studio Code.

## Running it

Run the executable from the `bin/[Release/Debug]` folder.

## License

This part of `falcon-bms-control` is using the GPL license because it is using a few classes of Helios that fall under the GPL license.