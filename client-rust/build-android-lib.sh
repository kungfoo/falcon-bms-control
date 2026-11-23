#!/bin/bash

cargo ndk -t arm64-v8a -t armeabi-v7a -P 27 rustc --lib --release -- --crate-type=cdylib
