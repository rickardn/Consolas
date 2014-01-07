using System;
using System.IO;
using System.Text;
using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class ConsoleApp_SystemTests
    {
        private TextWriter _consoleOutWriter;
        private StringBuilder _consoleOut;

        [SetUp]
        public void Setup()
        {
            _consoleOutWriter = Console.Out;
            _consoleOut = new StringBuilder();
            var testOut = new StringWriter(_consoleOut);
            Console.SetOut(testOut);
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(_consoleOutWriter);
        }

        [Test]
        public void Match_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new []{"-Parameter","foo"});
            StringAssert.Contains("SimpleCommand", _consoleOut.ToString());
        }

        [Test]
        public void Match_ExplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new []{"-DefaultProperty1", "foo", "-DefaultProperty2", "bar"});
            StringAssert.Contains("foo", _consoleOut.ToString());
            StringAssert.Contains("bar", _consoleOut.ToString());
        }

        [Test]
        public void Match_ImplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] { "foo", "bar" });
            StringAssert.Contains("foo", _consoleOut.ToString());
            StringAssert.Contains("bar", _consoleOut.ToString());
        }

        [Test]
        public void Match_MixingImplicitDefaultArgumentsWithExplicit_EndToEndTest1()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] { "-DefaultProperty1", "foo", "bar" });
            StringAssert.Contains("foo", _consoleOut.ToString());
            StringAssert.Contains("bar", _consoleOut.ToString());
        }

        [Test]
        public void Match_MixingImplicitDefaultArgumentsWithExplicit_EndToEndTest2()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] { "foo", "-DefaultProperty2", "bar" });
            StringAssert.Contains("foo", _consoleOut.ToString());
            StringAssert.Contains("bar", _consoleOut.ToString());
        }

        [Test]
        public void Match_OveridingImplicitDefaultArgumentWithExplicit_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] { "foo", "-DefaultProperty1", "bar" });
            StringAssert.Contains("bar", _consoleOut.ToString());
            StringAssert.DoesNotContain("foo", _consoleOut.ToString());
        }
    }
}
