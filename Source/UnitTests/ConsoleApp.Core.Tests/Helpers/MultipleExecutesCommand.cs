namespace Consolas.Core.Tests.Helpers
{
    public class MultipleExecutesArg1
    {
        public bool MultipleExecute1 { get; set; }
    }

    public class MultipleExecutesArg2
    {
        public bool MultipleExecute2 { get; set; }
    }

    public class MultipleExecutesCommand
    {
        public string Execute(MultipleExecutesArg1 args)
        {
            return "Execute 1";
        }

        public string Execute(MultipleExecutesArg2 args)
        {
            return "Execute 2";
        }
    }
}