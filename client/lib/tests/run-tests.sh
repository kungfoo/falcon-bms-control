#!/bin/bash

set -xe
busted -p '-test.lua' . --helper init.lua

