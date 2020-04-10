#!/bin/bash

lua-format -vi main.lua \
    mfds.lua icp.lua \
    components/icp.lua components/icp-button.lua \
    components/mfd-button.lua components/mfd.lua \
    components/switcher.lua
