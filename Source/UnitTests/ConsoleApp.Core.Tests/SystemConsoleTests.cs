using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class SystemConsoleTests
    {
        private StringBuilder _consoleOut;
        private TextWriter _outWriter;
        private SystemConsole _console;

        [SetUp]
        public void Setup()
        {
            _outWriter = Console.Out;
            _consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(_consoleOut));
            _console = new SystemConsole();
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_outWriter);
        }

        [Test]
        public void WriteLine_String_WritesToConsoleOut()
        {
            _console.WriteLine("foo");
            StringAssert.Contains("foo", _consoleOut.ToString());
        }

        [Test]
        public void WriteLine_Object_WritesToConsoleOut()
        {
            _console.WriteLine(1);
            StringAssert.Contains("1", _consoleOut.ToString());
        }

        [Test]
        public void Write_String_WritesToConsoleOut()
        {
            _console.Write("foo");
            StringAssert.Contains("foo", _consoleOut.ToString());
        }
    }
}
