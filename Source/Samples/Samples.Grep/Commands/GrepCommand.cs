using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Consolas.Core;
using Samples.Grep.Args;

namespace Samples.Grep.Commands
{
    public class GrepCommand : Command
    {
        public object Execute(GrepArgs args)
        {
            if (string.IsNullOrEmpty(args.Regex))
                return View("Default");

            var buffer = new StringBuilder();
            foreach (var line in File.ReadAllLines(args.FileName))
            {
                if (Regex.IsMatch(line, args.Regex))
                {
                    buffer.AppendLine(line);
                }
            }
            return buffer.ToString().Substring(0, buffer.Length - 1);
        }
    }
}
