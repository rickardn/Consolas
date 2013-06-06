using System;
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
        public void Invalid()
        {
            Assert.Throws<ArgumentException>(() => 
                Program.Main(new[] {"-Foo"}));
        }
    }
}
