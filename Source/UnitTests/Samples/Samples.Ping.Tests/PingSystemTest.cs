using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Samples.Ping.Tests
{
    [TestFixture]
    public class PingSystemTest
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
        public void Main_NoArgs_PrintsMessage()
        {
            Program.Main(new string[0]);
            StringAssert.Contains("Usage: ping", _consoleOut.ToString());
        }

        [Test]
        public void Main_ValidHost_RendersResult()
        {
            Program.Main(new [] {"-Host", "google.com"});
            StringAssert.Contains("Reply from google.com", _consoleOut.ToString());
        }

        [Test]
        public void Main_DefaultArgument_RendersResult()
        {
            Program.Main(new[] { "google.com" });
            StringAssert.Contains("Reply from google.com", _consoleOut.ToString());
        }
    }
}
