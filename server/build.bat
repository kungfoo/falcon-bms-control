nuget restore FalconBMSUniversalServer\FalconBMSUniversalServer.sln
msbuild F4TexSharedMem\F4TexSharedMem.sln -property:Configuration=Release -property:Platform=x64
msbuild FalconBMSUniversalServer\FalconBMSUniversalServer.sln -property:Configuration=Release -property:Platform=x64