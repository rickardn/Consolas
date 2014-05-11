using System;
using Consolas.Core.Tests.Helpers;
using Consolas.Mustache;
using NUnit.Framework;
using Should;
using SimpleInjector;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ConsoleAppTests : ConsoleTest
    {
        [Test]
        public void Match_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-Parameter", "foo"});
            StringAssert.Contains("SimpleCommand", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ExplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-DefaultProperty1", "foo", "-DefaultProperty2", "bar"});
            StringAssert.Contains("foo", ConsoleOut.ToString());
            StringAssert.Contains("bar", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ImplicitDefaultArguments_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"foo", "bar"});
            StringAssert.Contains("foo", ConsoleOut.ToString());
            StringAssert.Contains("bar", ConsoleOut.ToString());
            StringAssert.Contains("DefaultCommand", ConsoleOut.ToString());
        }

        [Test]
        public void Match_NoMatchingArgument_PrintsUsageMessageIfHelpCommandIsMissing()
        {
            var sut = new SimpleConsoleApp();
            sut.Main(new[] {"-NoMatchingArg"});
            StringAssert.Contains("Using", ConsoleOut.ToString());
        }

        [Test]
        public void Match_ArgumentWithoutCommand_ThrowsException()
        {
            var sut = new SimpleConsoleApp();

            Action match = () => sut.Main(new[] {"-ArgWithoutCommand", "foo"});

            match.ShouldThrow<NotImplementedException>();
        }

        [Test]
        public void Match_CommandWhichThrowsException_SameExceptionIsRethrown()
        {
            var sut = new SimpleConsoleApp();

            Action match = () => sut.Main(new[] {"-Throw"});

            match.ShouldThrow<Exception>();
        }

        [Test]
        public void Match_NoViewEngines_ThrowsException()
        {
            var sut = new SimpleConsoleApp();
            
            Action match = () => sut.Main(new []{"-ShowView"});

            match.ShouldThrow<ViewEngineException>(ex 
                => StringAssert.Contains("No view engines", ex.Message));
        }

        [Test]
        public void Match_NoViewFound_ThrowsException()
        {
            var sut = new SimpleConsoleAppWithViewEngine();

            Action match = () => sut.Main(new[] {"-ShowView"});

            match.ShouldThrow<ViewEngineException>(ex 
                => StringAssert.Contains("No view found", ex.Message));
        }
    }
}