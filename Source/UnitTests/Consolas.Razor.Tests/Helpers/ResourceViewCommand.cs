using Consolas.Core;

namespace Consolas.Razor.Tests.Helpers
{
    public class ResourceViewArgs
    {
        public string Message1 { get; set; }
    }

    public class ResourceViewCommand : Command
    {
        public object Execute(ResourceViewArgs args)
        {
            return View("ResourceView", new ViewModel
            {
                Message = args.Message1
            });
        }
    }
}