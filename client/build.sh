#!/bin/bash

pushd ../boon || exit
cargo build --release
popd || exit

../boon/target/release/boon build . --version 11.3