#!/bin/bash

find . -name '*.lua' -exec lua-format -vi {} \;

