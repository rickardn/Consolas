using Consolas.Core;
using NUnit.Framework;
using Samples.Grep.Args;
using Samples.Grep.Commands;
using Samples.Grep.Models;
using Should;

namespace Samples.Grep.Tests
{
    [TestFixture]
    public class VersionCommandTests
    {
        [Test]
        public void Execute_WhenCalled_ReturnsViewWithVersionAsModel()
        {
            var command = new VersionCommand();
            var versionArgs = new VersionArgs {Version = true};

            var result = (CommandResult) command.Execute(versionArgs);
            var model = (VersionViewModel) result.Model;

            result.ViewName.ShouldEqual("Version");
            model.Version.ShouldEqual("2.4.2");
        }
    }
}
