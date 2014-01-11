using ConsoleApp.Core;
using NSubstitute;
using NUnit.Framework;
using Samples.Ping.Commands;
using Samples.Ping.Models;

namespace Samples.Ping.Tests
{
    [TestFixture]
    public class PingCommandTests
    {
        [Test]
        public void ExpectedBehavior()
        {
            var console = Substitute.For<IConsole>();
            var thread = Substitute.For<IThreadService>();
            var command = new PingCommand(console, thread);
            command.Execute(new Args
            {
                Host = "google.com"
            });
            console.Received().WriteLine(Arg.Any<string>());
        }
    }
}
