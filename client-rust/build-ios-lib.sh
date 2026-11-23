#!/bin/bash

RUSTFLAGS="-C embed-bitcode=no" cargo rustc --lib --release --target aarch64-apple-ios

# Remove the dylib, because otherwise xcode tries to link against that
rm target/aarch64-apple-ios/release/libfalcon_bms_control_client.dylib
