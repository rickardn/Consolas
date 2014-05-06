using Consolas.Core.Tests.Helpers;
using NUnit.Framework;
using Should;
using SimpleInjector;

namespace Consolas.Core.Tests
{
    public class CommandTests
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
        }

        [Test]
        public void View_ModelAndViewAsFile_ReturnsViewWithViewModel()
        {
            var result = (CommandResult)_command.FileView(message: "foo bar");
            var model = (ViewModel)result.Model;

            result.ViewName.ShouldEqual("View");
            model.Message.ShouldEqual("foo bar");
        }

        [Test]
        public void View_ModelAndViewAsCompiledResource_ReturnsViewVithViewModel()
        {
            var result = (CommandResult)_command.ResourceView("baz zap");
            var model = (ViewModel)result.Model;

            result.ViewName.ShouldEqual("ResourceView");
            model.Message.ShouldEqual("baz zap");
        }

        [Test]
        public void View_NoModelAndViewAsFile_ReturnsView()
        {
            var result = (CommandResult)_command.FileView();
            
            result.ViewName.ShouldEqual("View");
            result.Model.ShouldBeNull();
        }

        [Test]
        public void Render_NoViewFound_ThrowsException()
        {
            Assert.Throws<ViewEngineException>(() 
                => _command.RenderFileView());
        }

        [Test]
        public void Render_ViewEnginesIsNull_ThrowsException()
        {
            _command.ViewEngines = null;
            Assert.Throws<ViewEngineException>(() 
                => _command.RenderFileView());
        }
    }
}