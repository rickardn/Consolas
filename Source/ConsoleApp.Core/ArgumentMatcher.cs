﻿using System;
using System.Collections.Generic;
using System.Linq;

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

        public object MatchToObject(string[] args, Type type)
        {
            var match = Match(args);
            if (match == null)
                return null;

            var instance = Activator.CreateInstance(type);
            var arguments = Parse(args);

            foreach (var argument in arguments)
            {
                var argumentName = RemovePrefix(argument.Key);

                var propertyInfo = type.GetProperty(argumentName);
                if (propertyInfo != null)
                {
                    if (propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyInfo.SetValue(instance, 
                            argument.Value.Value == null 
                            || bool.Parse(argument.Value.Value), null);
                    }
                    else
                    {
                        propertyInfo.SetValue(instance, argument.Value.Value, null);
                    }
                }
            }

            return instance;
        }

        public T MatchToObject<T>(string[] args)
        {
            return (T) MatchToObject(args, typeof (T));
        }

        public Type Match(string[] args)
        {
            return Match(Parse(args));
        }

        private static ArgumentSet Parse(string[] args)
        {
            var parser = new ArgumentParser();
            return parser.Parse(args, new ArgumentSet());
        }

        private Type Match(ArgumentSet arguments)
        {
            foreach (var type in Types)
            {
                bool isMatch = false;

                foreach (var argument in arguments)
                {
                    var argumentName = RemovePrefix(argument.Key);

                    var propertyInfo = type.GetProperty(argumentName);
                    if (propertyInfo != null)
                    {
                        isMatch = true;
                    }
                }

                if (isMatch)
                {
                    return type;
                }
            }

            return null;
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