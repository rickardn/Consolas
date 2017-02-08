#tool "nuget:?package=NUnit.Runners&version=2.6.3"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var buildTarget = configuration == "Release"
    ? "Clean;Rebuild"
    : "Build";

var solutionFile = File("./Source/Consolas.sln");

Task("Default")
    .IsDependentOn("Test");

Task("NuGetPush")
    .IsDependentOn("NuGetPack")
    .Does(() => 
{
    NuGetPush(File("./Consolas.0.6.2.nupkg"), new NuGetPushSettings {});
    NuGetPush(File("./Consolas.0.6.2.symbols.nupkg"), new NuGetPushSettings {});
    NuGetPush(File("./Consolas.Razor.0.6.0.nupkg"), new NuGetPushSettings {});
    NuGetPush(File("./Consolas.Razor.0.6.0.symbols.nupkg"), new NuGetPushSettings {});
});

Task("NuGetPack")
    .IsDependentOn("Copy")
    .Does(() => 
{
    NuGetPack("./NuGet/Consolas/Consolas.nuspec", new NuGetPackSettings
    {
        Symbols = true,
        OutputDirectory = "./NuGet"
    });
    NuGetPack("./NuGet/Consolas.Razor/Consolas.Razor.nuspec", new NuGetPackSettings
    {
        Symbols = true,
        OutputDirectory = "./NuGet"
    });
});

Task("Copy")
    .IsDependentOn("Test")
    .Does(() => 
{
    if (configuration != "Release")
        throw new Exception("Only package with release build!");

    var core = Directory("./NuGet/Consolas/lib/net40");
    CleanDirectory(core);
    CopyFileToDirectory(
        File("./Source/ConsoleApp.Core/bin/" + configuration + "/Consolas.Core.dll"), 
        core);
    CopyFileToDirectory(
        File("./Source/ConsoleApp.Core/bin/" + configuration + "/Consolas.Core.xml"), 
        core);

    var razor = Directory("./NuGet/Consolas.Razor/lib/net45");
    CleanDirectory(razor);
    CopyFileToDirectory(
        File("./Source/Consolas.Razor/bin/" + configuration + "/Consolas.Razor.dll"), 
        razor);
    CopyFileToDirectory(
        File("./Source/Consolas.Razor/bin/" + configuration + "/Consolas.Razor.xml"), 
        razor);
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    var testDlls = GetFiles("./**/UnitTests/**/bin/" + configuration + "/*.Tests.dll");
    NUnit(testDlls);
});

Task("Build")
    .Does(() => 
{
    MSBuild(solutionFile, config => config
        .SetConfiguration(configuration)
        .WithTarget(buildTarget));
});

RunTarget(target);