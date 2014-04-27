using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Consolas.Core
{
    public class ArgumentMatcher
    {
        public List<Type> Types { get; set; }

        public ArgumentMatcher()
        {
            Types = new List<Type>();
        }

        public T MatchToObject<T>(string[] args)
        {
            return (T) MatchToObject(args, typeof (T));
        }

        public object MatchToObject(string[] args, Type type)
        {
            ArgumentSet arguments = Parse(args);
            Type match = Match(arguments);
            
            if (match == null)
                return null;

            // TODO: should not use Command Builder to build argument object
            var instance = CommandBuilder.Current.GetCommandInstance(type);

            var i = 0;
            foreach (var argument in arguments)
            {
                if (argument.Value.IsDefault)
                {
                    var propertyInfo =
                        type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                            .ElementAt(i);

                    SetValue(propertyInfo, instance, argument);
                }
                else
                {
                    SetPropertyValue(type, argument, instance);
                }
                i++;
            }

            return instance;
        }

        public Type Match(string[] args)
        {
            return Match(Parse(args));
        }

        private void SetPropertyValue(Type type, KeyValuePair<string, Argument> argument, object instance)
        {
            var propertyInfo = GetPropertyInfo(type, argument.Key);
            if (propertyInfo != null)
            {
                SetValue(propertyInfo, instance, argument);
            }
        }

        private void SetValue(PropertyInfo propertyInfo, object instance, KeyValuePair<string, Argument> argument)
        {
            if (propertyInfo.PropertyType == typeof (bool))
            {
                var value = argument.Value.Value == null;
                if (!value)
                {
                    bool.TryParse(argument.Value.Value, out value);
                }
                propertyInfo.SetValue(instance, value, null);
            }
            else
            {
                propertyInfo.SetValue(instance, argument.Value.Value, null);
            }
        }

        private static ArgumentSet Parse(string[] args)
        {
            var parser = new ArgumentLL2Parser();
            return parser.Parse(args, new ArgumentSet());
        }

        private Type Match(ArgumentSet arguments)
        {
            bool useDefault = arguments.Any(a => a.Value.IsDefault);

            foreach (var type in Types)
            {
                var customAttributes = type.GetCustomAttributes(typeof (DefaultArgumentsAttribute), false);
                var isDefaultArgs = customAttributes.Length > 0;

                bool isMatch = isDefaultArgs && arguments.Count == 0;

                foreach (var argument in arguments)
                {
                    if (isDefaultArgs && useDefault && argument.Value.IsDefault)
                    {
                        isMatch = true;
                        argument.Value.IsDefault = true;
                        argument.Value.Value = argument.Key;
                    }
                    else
                    {
                        if (GetPropertyInfo(type, argument.Key) != null)
                        {
                            isMatch = true;
                        }
                    }
                }

                if (isMatch)
                {
                    return type;
                }
            }

            return null;
        }

        private static PropertyInfo GetPropertyInfo(Type type, string argumentName)
        {
            return type.GetProperty(argumentName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        }
    }
}