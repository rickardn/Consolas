namespace Consolas.Core.Tests.Helpers
{
    public class ConflictingArgs2
    {
        public int Arg { get; set; }
    }

    public class ConflictingArgs1
    {
        public int Arg { get; set; }
        public int Arg2 { get; set; }
    }

    public class ConflictingArgsCommand
    {
        public string Execute(ConflictingArgs1 args)
        {
            return "ConflictingArgs1";
        }

        public string Execute(ConflictingArgs2 args)
        {
            return "ConflictingArgs2";
        }
    }
}