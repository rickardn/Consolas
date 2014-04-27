using Consolas.Core.Tests.Helpers;
using NSubstitute;
using NUnit.Framework;

namespace Consolas.Core.Tests
{
    [TestFixture]
    public class CommandTests : ConsoleTest
    {
        private DescendantCommand _command;

        [SetUp]
        public void BeforeEach()
        {
            var viewEngineFactory = Substitute.For<IViewEngineFactory>();

            _command = new DescendantCommand()
            {
                ViewEngines = viewEngineFactory
            };

            viewEngineFactory
                .CreateEngine(Arg.Any<Command>(), Arg.Any<string>())
                .ReturnsForAnyArgs(new RazorViewEngine(_command));
        }

        [Test]
        public void Render_ViewAsFile_RendersView()
        {
            _command.RenderFileView(message: "foo bar");
            StringAssert.Contains("View foo bar", ConsoleOut.ToString());
        }

        [Test]
        public void Render_ViewAsCompiledResource_RendersView()
        {
            _command.RenderResourceView("baz zap");
            StringAssert.Contains("ResourceView baz zap", ConsoleOut.ToString());
        }
    }
}