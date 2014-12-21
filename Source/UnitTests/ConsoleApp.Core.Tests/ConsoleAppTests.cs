using System;
using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using Should;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class ConsoleAppTests : ConsoleTestBase
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
            StringAssert.Contains("Using: consolas", ConsoleOut.ToString());
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
        public void Match_RenderNoViewEngines_ThrowsException()
        {
            var sut = new SimpleConsoleApp();
            
            Action match = () => sut.Main(new []{"-RenderView"});

            match.ShouldThrow<ViewEngineException>(ex 
                => StringAssert.Contains("No view engines", ex.Message));
        }

        [Test]
        public void Match_RenderNoViewFound_ThrowsException()
        {
            var sut = new SimpleConsoleAppWithViewEngine();

            Action match = () => sut.Main(new[] {"-RenderView"});

            match.ShouldThrow<ViewEngineException>(ex 
                => StringAssert.Contains("No view found", ex.Message));
        }

        [Test]
        public void Match_ViewNoViewEngines_ThrowsException()
        {
            var sut = new SimpleConsoleApp();

            Action match = () => sut.Main(new[] { "-ShowView" });

            match.ShouldThrow<ViewEngineException>(ex
                => StringAssert.Contains("No view engines", ex.Message));
        }

        [Test]
        public void Match_ViewNoViewFound_ThrowsException()
        {
            var sut = new SimpleConsoleAppWithViewEngine();

            Action match = () => sut.Main(new[] { "-ShowView" });

            match.ShouldThrow<ViewEngineException>(ex
                => StringAssert.Contains("No view found", ex.Message));
        }

        [Test]
        public void Match_MultipleExecutes_EndToEndTest1()
        {
            var sut = new SimpleConsoleApp();

            sut.Main(new[] {"-MultipleExecute1"});

            StringAssert.Contains("Execute 1", ConsoleOut.ToString());
        }

        [Test]
        public void Match_MultipleExecutes_EndToEndTest2()
        {
            var sut = new SimpleConsoleApp();

            sut.Main(new[] { "-MultipleExecute2" });

            StringAssert.Contains("Execute 2", ConsoleOut.ToString());
        }

        [TestCase(new[] { "-Arg2", "-Arg" }, "ConflictingArgs1")]
        [TestCase(new[] { "-Arg", "-Arg2" }, "ConflictingArgs1")]
        [TestCase(new[] { "-Arg", "1", "-Arg2", "2" }, "ConflictingArgs1")]
        [TestCase(new[] { "-Arg2", "1", "-Arg", "2" }, "ConflictingArgs1")]
        public void Match_ConflictingArgs_EndToEndTest(string[] args, string expected)
        {
            var sut = new SimpleConsoleApp();

            sut.Main(args);

            StringAssert.Contains(expected, ConsoleOut.ToString());
        }

        [Test]
        public void Match_ConflictingNonDeterministicArgs_EndToEndTest()
        {
            var sut = new SimpleConsoleApp();

            sut.Main(new[] { "-Arg" });

            string output = ConsoleOut.ToString();

            // Should print exception message
            StringAssert.Contains("non deterministic", output);

            // Should print the usage msg or default view!
            StringAssert.Contains("Using:", output);
        }
    }
}