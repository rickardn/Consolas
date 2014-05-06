using Consolas.Core;
using Consolas.Razor.Tests.Helpers;
using NUnit.Framework;
using SimpleInjector;

namespace Consolas.Razor.Tests
{
    [TestFixture]
    public class CommandTests : ConsoleTest
    {
        private DescendantCommand _command;

        [SetUp]
        public void BeforeEach()
        {
           var container = new Container();
            _command = new DescendantCommand()
            {
                ViewEngines = new ViewEngineCollection(container)
            };
            _command.ViewEngines.Add<RazorViewEngine>();
        }

        [Test]
        public void RendersResourceView()
        {
            var program = new App();
            program.Main(new[] {"-Message1", "foobar"});

            StringAssert.Contains("ResourceView foobar", ConsoleOut.ToString());
        }

        [Test]
        public void RendersFileView()
        {
            var program = new App();
            program.Main(new[] { "-Message2", "foobar" });

            StringAssert.Contains("FileView foobar", ConsoleOut.ToString());
        }

        [Test]
        public void Render_ViewAsFile_ReturnsView()
        {
            _command.RenderFileView(message: "foo bar");
            StringAssert.Contains("View foo bar", ConsoleOut.ToString());
        }

        [Test]
        public void Render_ViewAsCompiledResource_ReturnsView()
        {
            _command.RenderResourceView("baz zap");
            StringAssert.Contains("ResourceView baz zap", ConsoleOut.ToString());
        }
    }
}