using Consolas.Core;
using $rootnamespace$.Args;

namespace $rootnamespace$.Commands
{
    public class HelpCommand : Command<HelpCommand>
    {
        public string Execute(HelpArgs args)
        {
            return "Using: $rootnamespace$.exe ...";
        }
    }
}