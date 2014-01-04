using System;
using NUnit.Framework;
using SampleConsoleApplication.Commands;

namespace SampleConsoleApplication.Tests
{
    [TestFixture]
    public class VersionCommandTests
    {
        [Test]
        public void Execute_ReturnsVersionString()
        {
            var command = new VersionCommand();
            var result = command.Execute(new VersionArgs());

            StringAssert.Contains("grep", result);
        }
    }
}
