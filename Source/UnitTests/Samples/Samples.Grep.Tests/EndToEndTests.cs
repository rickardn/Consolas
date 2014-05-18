using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Samples.Grep.Tests
{
    /// <summary>
    /// Try to keep end-to-end tests to a minimal only doing simple smoke testing ( http://en.wikipedia.org/wiki/Smoke_testing_(software) )
    /// Focus the testing on the commands in your application.
    /// </summary>
    [TestFixture]
    public class EndToEndTests
    {
        private StringBuilder _consoleOut;
        private TextWriter _outWriter;

        [SetUp]
        public void Setup()
        {
            _outWriter = Console.Out;
            _consoleOut = new StringBuilder();
            Console.SetOut(new StringWriter(_consoleOut));
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_outWriter);
        }

        [Test]
        public void NoArgs_PrintHelpMessage()
        {
            Program.Main(new string[0]);
            StringAssert.Contains("Usage", _consoleOut.ToString());
        }

        [Test]
        public void Grep()
        {
            Program.Main(new[] {"foo", "doc.txt"});
            StringAssert.Contains("foo bar baz", _consoleOut.ToString());
        }

        [Test]
        public void Grep_FileInCurrentPath()
        {
            Program.Main(new[] { "foo", @".\doc.txt" });
            StringAssert.Contains("foo bar baz", _consoleOut.ToString());
        }

        [Test]
        public void Version()
        {
            Program.Main(new []{ "-version"});
            StringAssert.Contains("2.4.2", _consoleOut.ToString());
        }
    }
}
