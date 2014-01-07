namespace ConsoleApp.Core
{
    public class ArgumentParser
    {
        public ArgumentSet Parse(string[] args, ArgumentSet arguments)
        {
            if (args == null || args.Length == 0)
            {
                return arguments;
            }

            for (int i = 0; i < args.Length; i++)
            {
                var arg = args[i];
                if (arg.StartsWith("-"))
                {
                    var parameterName = arg;
                    string parameterValue = null;
                    if (i + 1 <= args.Length)
                    {
                        try
                        {
                            parameterValue = args[i + 1];
                        } catch {}
                    }
                    arguments[parameterName] = new Argument
                    {
                        IsMatch = true,
                        Value = parameterValue
                    };
                    i++;
                }
                else
                {
                    arguments[arg] = new Argument
                    {
                        IsMatch = true
                    };
                }
            }

            return arguments;
        }
    }
}