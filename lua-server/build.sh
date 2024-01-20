#!/bin/bash

pushd ../boon || exit
cargo build --release
popd || exit

rm -rf release || exit

../boon/target/release/boon love download 11.5
../boon/target/release/boon build . --version 11.5

