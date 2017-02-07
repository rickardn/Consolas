using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Consolas.Core
{
    public class StringTokenizer
    {
        private static readonly Regex HasPrefix = 
            new Regex(@"^(?'prefix'\-\-|\-|/)(?'name'.*)$", RegexOptions.Compiled);

        public IEnumerable<string> Tokenize(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                yield break;
            }

            var match = HasPrefix.Match(text);
            if (match.Success)
            {
                var prefix = match.Groups["prefix"].Value;
                yield return prefix;

                var name = match.Groups["name"].Value;
                if (!string.IsNullOrEmpty(name))
                {
                    yield return name;
                }
            }
            else
            {
                yield return text;
            }
        }
    }
}