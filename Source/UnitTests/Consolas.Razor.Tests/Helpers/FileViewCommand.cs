using Consolas.Core;

namespace Consolas.Razor.Tests.Helpers
{
    public class FileViewArgs
    {
        public string Message2 { get; set; }
    }

    public class FileViewCommand : Command
    {
        public object Execute(FileViewArgs args)
        {
            return View("View", new ViewModel
            {
                Message = args.Message2
            });
        }
    }
}