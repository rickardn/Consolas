using System;

namespace Consolas.Core
{
    public class ArgumentLL2Parser
    {
        private Queue<string> _queue;
        private ArgumentSet _arguments;

        public ArgumentSet Parse(string[] args, ArgumentSet arguments)
        {
            if (args == null || args.Length == 0)
            {
                return arguments;
            }

            _queue = new Lexer().Tokenize(args);
            _arguments = arguments;

            Args();

            return _arguments;
        }

        private void Args()
        {
            if (Arg())
            do WhiteSpace();
            while (Arg());
        }

        private bool Arg()
        {
            if (Prefix) Statement();
            else Value(isDefault: true);
            return End();
        }

        private void WhiteSpace()
        {
            if (Tokens.WhiteSpace.IsMatch(Peek()))
            {
                Dequeue();
            }
        }

        private bool Prefix
        {
            get
            {
                if (Tokens.Prefix.IsMatch(Peek()))
                {
                    Dequeue();
                    return true;
                }
                return false;
            }
        }

        private bool Statement()
        {
            return
                ValueStatement()
             || BooleanStatement()
             || Error("statement");
        }

        private bool ValueStatement()
        {
            return 
                AssignmentStatement()
             || PropertyStatement();
        }

        private bool BooleanStatement()
        {
            var booleanStatement = Tokens.BooleanStatement.Match(Peek());

            if (booleanStatement.Success)
            {
                Dequeue();

                var name = booleanStatement.Groups["name"]?.Value;
                var @operator = booleanStatement.Groups["operator"]?.Value;

                if (!string.IsNullOrEmpty(@operator))
                {
                    _queue.DeDequeue(@operator);
                }

                return BooleanArgument(name);
            }

            return false;
        }

        private bool AssignmentStatement()
        {
            var assignmentStatement = Tokens.AssigmentStatement.Match(Peek());

            if (assignmentStatement.Success)
            {
                Dequeue();

                var name = assignmentStatement.Groups["name"]?.Value;
                var op = assignmentStatement.Groups["operator"]?.Value;
                var value = assignmentStatement.Groups["value"]?.Value;

                if (!string.IsNullOrEmpty(value))
                {
                    _queue.DeDequeue(value);
                }
                _queue.DeDequeue(op);

                return ValueArgument(name);
            }
            return false;
        }

        private bool PropertyStatement()
        {
            var name = Peek();
            if (Tokens.Name.IsMatch(name))
            {
                Dequeue();

                if (Tokens.WhiteSpace.IsMatch(Peek()))
                {
                    return ValueArgument(name);
                }

                _queue.DeDequeue(name);
            }
            return false;
        }

        private void Value(string name = null, string val = null, 
            bool isDefault = false)
        {
            var value = val ?? GetValue();
            _arguments.Add(name ?? value, new Argument
            {
                IsMatch = true,
                Value = value,
                IsDefault = isDefault
            });
        }

        private string GetValue()
        {
            var value = Peek();
            if (IsValue(value))
            {
                return Dequeue();
            }
            throw Error(value, "value");
        }

        private static bool IsValue(string value)
        {
            return Tokens.Value.IsMatch(value);
        }

        private bool ValueArgument(string name)
        {
            return 
                AssingmentWithoutOperator(name) 
             || AssinmentWithOperator(name);
        }

        private bool AssingmentWithoutOperator(string name)
        {
            var @operator = Peek();

            if (Tokens.WhiteSpace.IsMatch(@operator))
            {
                Operator();

                if (!IsValue(Peek()))
                {
                    _queue.DeDequeue(@operator);
                    _queue.DeDequeue(name);
                    return false;
                }
                Value(name: name);

                return true;
            }

            return false;
        }

        private bool AssinmentWithOperator(string name)
        {
            var @operator = Peek();

            if (Tokens.AssignmentOperator.IsMatch(@operator))
            {
                Operator();

                if (IsValue(Peek()))
                {
                    Value(name: name);
                }
                else
                {
                    Value(name: name, val: "");
                }
                return true;
            }

            return false;
        }

        private bool BooleanArgument(string name)
        {
            var value = true.ToString();

            var boolOperator = Peek();
            if (Tokens.BoolOperator.IsMatch(boolOperator))
            {
                boolOperator = Dequeue();
                boolOperator = (boolOperator == "+").ToString();
                value = boolOperator;
            }

            _arguments.Add(name, new Argument
            {
                Value = value,
                IsMatch = true
            });

            return true;
        }

        private void Operator()
        {
            Dequeue();
        }

        private bool Error(string name)
        {
            throw Error(Peek(), name);
        }

        private Exception Error(string token, string name)
        {
            return new Exception(string.Format("Couldn't parse '{0}' as {1}", token, name));
        }

        private bool End()
        {
            return _queue.Count > 0;
        }

        private string Dequeue()
        {
            return _queue.Dequeue();
        }

        private string Peek()
        {
            return _queue.Count > 0
                ? _queue.Peek()
                : "";
        }
    }
}