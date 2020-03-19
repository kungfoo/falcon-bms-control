#!/bin/bash

echo "Removing old generated sources"
rm -rf client/flatbuffers
rm -rf server/flatbuffers

echo "Generating new flatbuffers source files"
tools/flatc.exe --lua -o client/flatbuffers flatbuffers/*
tools/flatc.exe --csharp -o server/flatbuffers flatbuffers/*