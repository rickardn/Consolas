using System.Text.RegularExpressions;

namespace Consolas.Core
{
    public static class Tokens
    {
        public static readonly Regex Prefix = new Regex(@"^(\-\-|\-|/)$", RegexOptions.Compiled);
        public static readonly Regex Operator = new Regex(@"^[=:]$", RegexOptions.Compiled);
        public static readonly Regex BoolOperator = new Regex(@"^[\-\+]$", RegexOptions.Compiled);
        public static readonly Regex Name = new Regex(@"^[A-Za-z][A-Za-z0-9]*$", RegexOptions.Compiled);
        public static readonly Regex Value = new Regex(@"^[^\\ \-/+=:](.*)$", RegexOptions.Compiled);
        public static readonly Regex WhiteSpace = new Regex(@"^( +)$", RegexOptions.Compiled);
        public static readonly string[] Operators = {" ", "--", "-", "/", "+", ":", "="};
    }
}