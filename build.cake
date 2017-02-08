#tool "nuget:?package=NUnit.Runners&version=2.6.3"

var target = Argument("Target", "Default");
var configuration = Argument("Configuration", "Release");
var buildTarget = configuration == "Release"
    ? "Clean;Rebuild"
    : "Build";

var solutionFile = File("./Source/Consolas.sln");

Task("Default")
    .IsDependentOn("Test");

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