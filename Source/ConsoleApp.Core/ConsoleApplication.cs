using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ConsoleApp.Core
{
    public class ConsoleApplication
    {
        protected static void IgnoreCase()
        {
            
        }

        protected static void Match(string[] args)
        {
            var matcher = new ArgumentMatcher();

            var assembly = Assembly.GetCallingAssembly();
            var argTypes = assembly.GetTypes();
            matcher.Types = argTypes.ToList();

            var type = matcher.Match(args);

            var commands = assembly.GetTypes()
                                   .Where(x => x.GetMethods().Any(
                                       m => m.GetParameters().Any(
                                           p => p.ParameterType == type)));

            Type commandType = commands.FirstOrDefault();
            if (commandType != null)
            {
                var command = Activator.CreateInstance(commandType);
                var arg = matcher.MatchToObject(args, type);

                var methodInfo = commandType.GetMethod("Execute");
                var result = methodInfo.Invoke(command, new[] {arg});
                Console.WriteLine(result);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}