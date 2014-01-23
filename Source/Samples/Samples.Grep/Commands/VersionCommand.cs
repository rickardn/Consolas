using Consolas.Core;
using Samples.Grep.Args;

namespace Samples.Grep.Commands
{
    public class VersionCommand : Command
    {
        public void Execute(VersionArgs args)
        {
            Render("Version");
        }
    }
}