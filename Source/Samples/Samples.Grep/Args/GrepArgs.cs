using Consolas.Core;

namespace Samples.Grep.Args
{
    [DefaultArguments]
    public class GrepArgs
    {
        public string Regex { get; set; }
        public string FileName { get; set; }
    }
}