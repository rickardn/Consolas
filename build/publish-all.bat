SET MSBUILD=C:\Windows\Microsoft.NET\Framework\v4.0\MSBuild.exe

%MSBUILD% build.msbuild "/t:Build;Copy;NuGetPack;NuGetPush"