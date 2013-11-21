using ConsoleApp.Core;
using NUnit.Framework;

namespace SampleConsoleApplication.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void Help_PrintsMessage()
        {
            Program.Main(new[] {"-Help"});
        }

        [Test]
        public void Default()
        {
            Program.Main(new[] {"-Foo"});
        }
    }
}