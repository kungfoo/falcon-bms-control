#!/bin/bash
rm -rf build
mkdir -p build
zip -r build/game.love . -x build
