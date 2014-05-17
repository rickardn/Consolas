using Consolas.Core;
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
            var thread = Substitute.For<IThreadService>();
            var command = new PingCommand(thread);

            // TODO: Implement PingCommand Test
            //command.Execute(new PingArgs
            //{
            //    Host = "google.com"
            //});
        }
    }
}
