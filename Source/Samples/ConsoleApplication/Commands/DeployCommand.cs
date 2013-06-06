using ConsoleApp.Core;

namespace SampleConsoleApplication.Commands
{
    public class DeployCommand : ICommand
    {
         public string Execute(DeployArgs args)
         {
             return string.Format("{0} {1}", args.Service, args.Target);
         }
    }
}