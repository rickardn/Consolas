#tool "nuget:?package=NUnit.Runners&version=2.6.3"
#tool "nuget:?package=GitVersion.CommandLine"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var buildTarget = configuration == "Release"
    ? "Clean;Rebuild"
    : "Build";

var solutionFile = File("./Source/Consolas.sln");
var bag = new Dictionary<string, string>();

Task("Default")
    .IsDependentOn("Test");

Task("NuGetPush")
	.IsDependentOn("GetVersion")
    .IsDependentOn("NuGetPack")
    .Does(() =>
{
	NuGetPush(GetFiles("./NuGet/Consolas." + bag["NuGetVersion"] + ".nupkg"),
	new NuGetPushSettings {
		Source = "https://www.nuget.org/api/v2/package"
	});
});

Task("NuGetPack")
    .IsDependentOn("Copy")
	.IsDependentOn("GetVersion")
    .Does(() =>
{
	var nuGetPackSettings = new NuGetPackSettings {
        Version                 = bag["NuGetVersion"],
        OutputDirectory			= "./NuGet"
    };

    NuGetPack("./NuGet/Consolas/Consolas.nuspec", nuGetPackSettings);
});

Task("GetVersion")
	.Does(() =>
{
	var result = GitVersion();
	bag["NuGetVersion"] = result.NuGetVersion;
	Information("Version: " + bag["NuGetVersion"]);
});

Task("Copy")
    .IsDependentOn("Test")
    .Does(() =>
{
    if (configuration != "Release")
        throw new Exception("Only package with release build!");

    var core = Directory("./NuGet/Consolas/lib/net40");
    CleanDirectory(core);
    CopyFiles(
        GetFiles("./Source/ConsoleApp.Core/bin/" + configuration + "/Consolas.Core.*"),
        core);
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