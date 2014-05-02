using Consolas.Core.Consolas.Mustache;
using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using SimpleInjector;

namespace Consolas.Core.Tests.Mustache
{
    [TestFixture]
    public class MustacheCommandTests : ConsoleTest
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
            _command.ViewEngines.Add<MustacheViewEngine>();
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
