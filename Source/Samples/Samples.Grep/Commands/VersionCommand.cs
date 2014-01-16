using ConsoleApp.Core;
using Samples.Grep.Args;

namespace Samples.Grep.Commands
{
    public class VersionCommand : Command
    {
        public string Execute(VersionArgs args)
        {
            Render<object>("Version", null);
            return "";
        }
    }
}