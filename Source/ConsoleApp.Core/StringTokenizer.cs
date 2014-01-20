using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp.Core
{
    public class StringTokenizer
    {
        private string[] _operators = { "--", "-", "/", "+" };

        public string[] Operators
        {
            get { return _operators; }
            set { _operators = value; }
        }

        public IEnumerable<string> Tokenize(string text)
        {
            return Tokenize(text, Operators);
        }

        private IEnumerable<string> Tokenize(string text, string[] operators)
        {
            if (operators.Length == 0)
            {
                if (text.Length > 0)
                    yield return text;
                yield break;
            }

            var op = operators[0];

            var i = text.IndexOf(op, StringComparison.Ordinal);
            if (i >= 0)
            {
                var head = text.Substring(0, i);
                if (head.Length > 0)
                {
                    var ops = operators.Skip(1).ToArray();

                    foreach (var token in Tokenize(head, ops))
                    {
                        yield return token;
                    }
                }
                
                yield return op;

                var tail = text.Substring(i + op.Length);
                if (tail.Length > 0)
                {
                    foreach (var token in Tokenize(tail))
                    {
                        yield return token;
                    }
                }
            }
            else if (text.Length > 0)
            {
                var ops = operators.Skip(1).ToArray();

                foreach (var token in Tokenize(text, ops))
                {
                    yield return token;
                }
                
            }
        }
    }
}