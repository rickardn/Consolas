using System.IO;
using System.Text;
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
            var buffer = new StringBuilder();
            foreach (var line in File.ReadAllLines(args.FileName))
            {
                if (Regex.IsMatch(line, args.Regex))
                {
                    buffer.AppendLine(line);
                }
            }
            return buffer.ToString();
        }
    }
}