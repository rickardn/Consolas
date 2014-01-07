using System.IO;
using System.Text.RegularExpressions;
using ConsoleApp.Core;

namespace Samples.Grep.Commands
{
    [DefaultArguments]
    public class GrepArgs
    {
        public string Regex { get; set; }
        public string FileName { get; set; }
    }

    public class GrepCommand
    {
        private readonly IConsole _console;

        public GrepCommand(IConsole console)
        {
            _console = console;
        }

        public string Execute(GrepArgs args)
        {
            var text = File.ReadAllText(args.FileName);
            return Regex.Match(text, args.Regex).Value;
        }
    }
}