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
            if (Prefix)
                Statement();
            else
                Value(isDefault: true);

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
                var prefix = Peek();
                if (Tokens.Prefix.IsMatch(prefix))
                {
                    Dequeue();
                    return true;
                }
                return false;
            }
        }

        private bool Statement()
        {
            var name = Name();
            return 
                ValueStatement(name) 
             || BooleanStatement(name);
        }

        private void Value(string name = null, string val = null, bool isDefault = false)
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

        private bool IsValue(string value)
        {
            return Tokens.Value.IsMatch(value);
        }

        private bool ValueStatement(string name)
        {
            var @operator = Peek();

            if (@operator == " ")
            {
                @operator = Dequeue();
                if (!IsValue(Peek()))
                {
                    _queue.DeDequeue(@operator);
                    return false;
                }
                Value(name: name);
            }
            else if (Tokens.Operator.IsMatch(@operator))
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
            }
            else
            {
                return false;
            }

            return true;
        }

        private bool BooleanStatement(string name)
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

        private string Name()
        {
            if (_queue.Count == 0)
                throw UnexpectedEnd("name");
            
            var name = Peek();

            if (Tokens.Name.IsMatch(name))
            {
                return Dequeue();
            }
            throw Error(name, "name");
        }

        private void Operator()
        {
            Dequeue();
        }

        private Exception UnexpectedEnd(string name)
        {
            return new Exception(string.Format("Unexpected end of string at {0}", name));
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