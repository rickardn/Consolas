using System.Collections.Generic;

namespace ConsoleApp.Core
{
    public class ArgumentSet : Dictionary<string, Argument>
    {
        public new Argument this[string key]
        {
            get
            {
                Argument argument;
                return TryGetValue(key, out argument)
                           ? argument
                           : Argument.NoMatch;
            }
            set { base[key] = value; }
        }
    }
}