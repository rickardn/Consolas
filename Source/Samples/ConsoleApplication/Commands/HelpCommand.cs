using ConsoleApp.Core;

namespace SampleConsoleApplication.Commands
{
    public class HelpCommand : ICommand
    {
        public string Execute(HelpArgs args)
        {
            return "Help";
        }
    }
}