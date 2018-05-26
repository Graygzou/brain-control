#! /bin/sh

# This file is based on an example by JonathanPorta. See the original example here: 
#	https://github.com/JonathanPorta/ci-build

# Unity used this website to host Unity archive
BASE_URL=http://netstorage.unity3d.com/unity

# We need that version 2017.3.1f1 here
HASH=fc1d3344e6ea
VERSION=5.4.1f1

# This link changes from time to time. I haven't found a reliable hosted installer package for doing regular
# installs like this. You will probably need to grab a current link from: http://unity3d.com/get-unity/download/archive

echo 'Downloading from https://download.unity3d.com/download_unity/fc1d3344e6ea/Windows64EditorInstaller/UnitySetup64-2017.3.1f1.exe: '
curl -o Unity.exe https://download.unity3d.com/download_unity/fc1d3344e6ea/Windows64EditorInstaller/UnitySetup64-2017.3.1f1.exe

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.exe -target /
