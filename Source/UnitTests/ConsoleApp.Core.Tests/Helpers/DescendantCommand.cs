namespace Consolas.Core.Tests.Helpers
{
    public class DescendantCommand : Command
    {
        public object FileView(string message)
        {
            var model = new ViewModel
            {
                Message = message
            };
            return View("View", model);
        }

        public object FileView()
        {
            return View("View");
        }

        public void RenderFileView()
        {
            Render("View");
        }

        public void RenderNonExistantView()
        {
            Render("Missing");
        }

        public object ResourceView(string message)
        {
            var model = new ViewModel
            {
                Message = message
            };
            return View("ResourceView", model);
        }
    }
}