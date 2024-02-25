#!/bin/bash

set -e

projects=("flup" "client" "server" "lua-server")

for project in "${projects[@]}"
do
  pushd "${project}"
    ./format.sh
  popd
done



