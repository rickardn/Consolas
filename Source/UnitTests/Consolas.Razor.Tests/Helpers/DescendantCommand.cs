using Consolas.Core;

namespace Consolas.Razor.Tests.Helpers
{
    public class DescendantCommand : Command
    {
        public void RenderFileView(string message)
        {
            Render("View", new ViewModel
            {
                Message = message
            });
        }

        public void RenderResourceView(string message)
        {
            Render("ResourceView", new ViewModel
            {
                Message = message
            });
        }
    }
}