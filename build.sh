#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore
dotnet build ./test/Veggerby.Algorithm.Tests -c Release
dotnet test ./test/Veggerby.Algorithm.Tests -c Release

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "build-%04d" $revision)

dotnet pack ./src/Veggerby.Algorithm -c Release -o $artifactsFolder --version-suffix=$revision