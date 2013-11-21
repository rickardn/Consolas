using NUnit.Framework;
using SampleConsoleApplication.Commands;

namespace SampleConsoleApplication.Tests
{
    [TestFixture]
    public class HelpCommandTests
    {
        [Test]
        public void ExpectedBehavior()
        {
            var command = new HelpCommand();
            var args = new HelpArgs();
            var execute = command.Execute(args);
            StringAssert.Contains("Help", execute);
        }
    }
}
