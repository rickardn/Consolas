using Consolas.Core.Tests.Helpers;
using Consolas.Mustache;
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
        public void Render_NoModelAndViewAsFile_ReturnsView()
        {
            _command.RenderFileView();
            StringAssert.Contains("FileView", ConsoleOut.ToString());
        }
    }
}
