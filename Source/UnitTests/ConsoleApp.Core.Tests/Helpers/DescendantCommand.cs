namespace ConsoleApp.Core.Tests.Helpers
{
    public class DescendantCommand : Command
    {
        public void RenderFileView(string message)
        {
            var model = new ViewModel
            {
                Message = message
            };
            Render("View", model);
        }

        public void RenderResourceView(string message)
        {
            var model = new ViewModel
            {
                Message = message
            };
            Render("ResourceView", model);
        }
    }
}