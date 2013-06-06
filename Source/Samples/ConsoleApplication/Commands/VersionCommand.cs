using ConsoleApp.Core;

namespace SampleConsoleApplication.Commands
{
    public class VersionCommand : ICommand
    {
        public string Execute(VersionArgs args)
        {
            return "1.0";
        }
    }
}