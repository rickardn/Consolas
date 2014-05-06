using Consolas.Core;
using Samples.Grep.Args;
using Samples.Grep.Models;

namespace Samples.Grep.Commands
{
    public class VersionCommand : Command
    {
        public object Execute(VersionArgs args)
        {
            var model = new VersionViewModel
            {
                Version = "2.4.2"
            };

            return View("Version", model);
        }
    }
}