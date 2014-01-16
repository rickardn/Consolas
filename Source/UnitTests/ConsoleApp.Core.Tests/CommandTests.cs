using ConsoleApp.Core.Tests.Helpers;
using NUnit.Framework;

namespace ConsoleApp.Core.Tests
{
    [TestFixture]
    public class CommandTests : ConsoleTest
    {
        private DescendantCommand _command;

        [SetUp]
        public void BeforeEach()
        {
            _command = new DescendantCommand();
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