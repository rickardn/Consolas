using System.ComponentModel;
using Consolas.Core.Tests.Helpers;
using Consolas.Razor;
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
            _command = new DescendantCommand()
            {
                ViewEngines = new ViewEngineCollection()
            };
            _command.ViewEngines.Add<RazorViewEngine>();
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