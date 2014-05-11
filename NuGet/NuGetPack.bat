SET NUGET=..\Source\.nuget\nuget
%NUGET% pack Consolas\Consolas.nuspec -symbols
%NUGET% pack Consolas.Razor\Consolas.Razor.nuspec -symbols