namespace SampleConsoleApplication.Commands
{
    public class DeployCommand
    {
        public string Execute(DeployArgs args)
        {
            return string.Format("{0} {1}", args.Service, args.Target);
        }

        public string SubCommand(DeployArgs args)
        {
            return "Subcommand";
        }
    }
}