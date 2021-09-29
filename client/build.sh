#!/bin/bash

pushd ../boon || exit
cargo build --release
popd || exit

../boon/target/release/boon love download 11.3
../boon/target/release/boon build . --version 11.3

mv release/Falcon\ Bms\ Control.love release/game.love