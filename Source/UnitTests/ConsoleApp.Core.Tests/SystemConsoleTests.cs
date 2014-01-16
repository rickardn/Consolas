using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class SystemConsoleTests : ConsoleTest
    {
        private SystemConsole _console;

        [SetUp]
        public void BeforeEach()
        {
            _console = new SystemConsole();
        }

        [Test]
        public void WriteLine_String_WritesToConsoleOut()
        {
            _console.WriteLine("foo");
            StringAssert.Contains("foo", ConsoleOut.ToString());
        }

        [Test]
        public void WriteLine_Object_WritesToConsoleOut()
        {
            _console.WriteLine(1);
            StringAssert.Contains("1", ConsoleOut.ToString());
        }

        [Test]
        public void Write_String_WritesToConsoleOut()
        {
            _console.Write("foo");
            StringAssert.Contains("foo", ConsoleOut.ToString());
        }
    }
}
