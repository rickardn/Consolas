namespace Consolas.Core.Tests.Helpers
{
    public class CommandWithDefaultArgs
    {
        public string Execute(DefaultParameters args)
        {
            return args.DefaultProperty1 + " " + args.DefaultProperty2 + " DefaultCommand";
        }
    }
}