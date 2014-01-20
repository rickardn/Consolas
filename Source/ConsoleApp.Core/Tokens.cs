using System.Text.RegularExpressions;

namespace ConsoleApp.Core
{
    public class Tokens
    {
        public static readonly Regex Prefix = new Regex("^--|-|/", RegexOptions.Compiled);
        public static readonly Regex Operator = new Regex("=|:", RegexOptions.Compiled);
        public static readonly Regex BoolOperator = new Regex("-|\\+", RegexOptions.Compiled);
        public static readonly Regex Name = new Regex("^[A-Za-z_]{1}[A-Za-z0-9]*$", RegexOptions.Compiled);
        public static readonly Regex Value = new Regex("^[^ -/\\+=:]{1}.*$", RegexOptions.Compiled);
        public static readonly Regex WhiteSpace = new Regex("^ $", RegexOptions.Compiled);
    }
}