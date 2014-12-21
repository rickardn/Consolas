using System.Collections.Generic;
using System.Text;

namespace Consolas.Core
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

        public override string ToString()
        {
            var builder = new StringBuilder("{");
            var i = 0;
            foreach (var key in Keys)
            {
                if (i > 0) builder.Append(", ");

                builder.Append("{");
                Argument arg = this[key];
                builder.AppendFormat("name:\"{3}\", value:\"{0}\", isMatch:{1}, isDefault:{2}", 
                    arg.Value, arg.IsMatch, arg.IsDefault, key);

                builder.Append("}");
                i++;
            }

            builder.Append("}");
            return builder.ToString();
        }
    }
}