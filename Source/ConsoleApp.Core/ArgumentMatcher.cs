using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConsoleApp.Core
{
    public class ArgumentMatcher
    {
        public List<Type> Types { get; set; }
        public List<string> Prefixes { get; set; }

        public ArgumentMatcher()
        {
            Prefixes = new List<string> {"--", "-", "/"};
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
            var argumentName = RemovePrefix(argument.Key);

            var propertyInfo = GetPropertyInfo(type, argumentName);
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
            var parser = new ArgumentParser();
            return parser.Parse(args, new ArgumentSet());
        }

        private Type Match(ArgumentSet arguments)
        {
            bool useDefault = arguments.Count == 0 || !HasPrefix(arguments.First().Key);

            foreach (var type in Types)
            {
                var customAttributes = type.GetCustomAttributes(typeof (DefaultArgumentsAttribute), false);
                var isDefaultArgs = customAttributes.Length > 0;

                bool isMatch = false;

                foreach (var argument in arguments)
                {
                    if (isDefaultArgs && useDefault && !HasPrefix(argument.Key))
                    {
                        isMatch = true;
                        argument.Value.IsDefault = true;
                        argument.Value.Value = argument.Key;
                    }
                    else
                    {
                        var argumentName = RemovePrefix(argument.Key);

                        if (GetPropertyInfo(type, argumentName) != null)
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

        private bool HasPrefix(string key)
        {
            return Prefixes.Any(key.StartsWith);
        }

        private static PropertyInfo GetPropertyInfo(Type type, string argumentName)
        {
            return type.GetProperty(argumentName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
        }

        private string RemovePrefix(string key)
        {
            string[] arg = {key};

            foreach (var prefix in Prefixes.Where(prefix => arg[0].StartsWith(prefix)))
            {
                arg[0] = arg[0].Substring(prefix.Length);
            }
            return arg[0];
        }
    }
}