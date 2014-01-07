using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace ConsoleApp.Core
{
    public abstract class ConsoleAppBase
    {
        private const string InitMethodName = "Match";
        private const string ExecuteMethod = "Execute";
        internal static Container Container;
        private ArgumentMatcher _argumentMatcher;

        public virtual void Configure(Container container) {}

        protected static void Match(string[] args)
        {
            #region Do not refactor
            Assembly callingAssembly = Assembly.GetCallingAssembly();
            #endregion

            ConsoleAppBase app = CreateConsoleApp();
            Configure(app);

            CommandType commandType = app.FindCommandType(args, callingAssembly);

            if (commandType != null)
            {
                CommandSet command = app.FindCommand(args, commandType);
                app.ExecuteCommand(commandType, command);
            }
        }

        private static void Configure(ConsoleAppBase app)
        {
            Container = new Container();
            CommandBuilder.Current.SetCommandFactory(
                new SimpleInjectorCommandFactory(Container));
            app.Configure(Container);
        }

        private static ConsoleAppBase CreateConsoleApp()
        {
            ConsoleAppBase consoleAppBase = null;
            var stack = new StackTrace();
            var stackFrames = stack.GetFrames();

            // ReSharper disable PossibleNullReferenceException
            foreach (StackFrame frame in stackFrames)
            // ReSharper restore PossibleNullReferenceException
            {
                var method = frame.GetMethod();
                if (method.Name != InitMethodName)
                {
                    var declaringType = frame.GetMethod().DeclaringType;
                    if (declaringType != null && declaringType.IsAbstract)
                        continue;

                    if (declaringType != null)
                    {
                        consoleAppBase = Activator.CreateInstance(declaringType) as ConsoleAppBase;
                        break;
                    }
                }
            }
            return consoleAppBase;
        }

        private CommandType FindCommandType(string[] args, Assembly callingAssembly)
        {
            var argTypes = callingAssembly.GetTypes().ToList();
            _argumentMatcher = new ArgumentMatcher
            {
                Types = argTypes
            };
            var argsType = _argumentMatcher.Match(args);
            var commandType = FindMatchingCommandType(argTypes, argsType);
            if (commandType != null)
            {
                return new CommandType
                {
                    Command = commandType,
                    Args = argsType
                };
            }
            return null;
        }

        private CommandSet FindCommand(string[] args, CommandType commandType)
        {
            object command = CommandBuilder.Current.GetCommandInstance(commandType.Command);
            object arg = _argumentMatcher.MatchToObject(args, commandType.Args);

            return new CommandSet
            {
                Command = command,
                Args = arg
            };
        }

        private void ExecuteCommand(CommandType commandType, CommandSet command)
        {
            var methodInfo = commandType.Command.GetMethod(ExecuteMethod);
            // TODO
            var result = methodInfo.Invoke(command.Command, new[] { command.Args });
            Console.WriteLine(result);
        }

        private Type FindMatchingCommandType(IEnumerable<Type> argTypes, Type argsType)
        {
            return argTypes.FirstOrDefault(x => x.GetMethods().Any(m => m.GetParameters().Any(p => p.ParameterType == argsType)));
        }

        private class CommandType
        {
            public Type Command { get; set; }
            public Type Args { get; set; }
        }

        private class CommandSet
        {
            public object Command { get; set; }
            public object Args { get; set; }
        }
    }
}