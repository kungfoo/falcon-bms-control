nuget restore FalconBMSUniversalServer\FalconBMSUniversalServer.sln
msbuild F4TexSharedMem\F4TexSharedMem.sln -property:Configuration=Debug -property:Platform=x64
msbuild FalconBMSUniversalServer\FalconBMSUniversalServer.sln -property:Configuration=Debug -property:Platform=x64