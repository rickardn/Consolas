using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Samples.Grep.Tests
{
    [TestFixture]
    public class GrepEndToEndTests
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
        public void ExpectedBehavior()
        {
            Program.Main(new[] {"foo", "doc.txt"});
            
            StringAssert.Contains("foo", _consoleOut.ToString());
            StringAssert.Contains("foo bar baz", _consoleOut.ToString());
        }
    }
}
