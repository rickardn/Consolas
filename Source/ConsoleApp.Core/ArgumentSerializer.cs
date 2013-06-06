using System;
using System.Reflection;
using System.Text;

namespace ConsoleApp.Core
{
    public class ArgumentSerializer
    {
        public ArgumentSerializer()
        {
            Prefixes = new[] {"-"};
        }

        public string SerializeToString<T>()
        {
            return SerializeToString(typeof (T));
        }

        public string SerializeToString(Type type)
        {
            var stringBuilder = new StringBuilder();
            var properties = type.GetProperties();
            var propertiesCount = properties.Length;

            for (int i = 0; i < propertiesCount; i++)
            {
                PropertyInfo property = properties[i];
                stringBuilder.Append(Prefixes[0]);
                stringBuilder.Append(property.Name);

                if (i < propertiesCount - 1)
                {
                    stringBuilder.Append(" ");
                }
            }

            return stringBuilder.ToString();
        }

        public string SerializeToString(string[] args)
        {
            var stringBuilder = new StringBuilder();
            foreach (var arg in args)
            {
                if (arg.Length > 1 && arg[0].ToString().Equals( Prefixes[0]))
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(" ");
                    }

                    stringBuilder.Append(arg);                    
                }
            }
            return stringBuilder.ToString();
        }

        public string[] Prefixes { get; set; }
    }
}