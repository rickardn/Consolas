using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace ConsoleApp.Core
{
    public abstract class ConsoleApp
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

            ConsoleApp app = CreateConsoleApp();
            Configure(app);

            CommandType commandType = app.FindCommandType(args, callingAssembly);

            if (commandType != null)
            {
                CommandSet command = app.FindCommand(args, commandType);
                app.ExecuteCommand(commandType.Command, command);
            }
            else
            {
                // TODO
                Console.WriteLine("Using: {0}.exe ...", callingAssembly.GetName().Name.ToLower());
            }
        }

        private static void Configure(ConsoleApp app)
        {
            Container = new Container();
            CommandBuilder.Current.SetCommandFactory(
                new SimpleInjectorCommandFactory(Container));
            app.Configure(Container);
        }

        private static ConsoleApp CreateConsoleApp()
        {
            ConsoleApp consoleApp = null;
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
                        consoleApp = Activator.CreateInstance(declaringType) as ConsoleApp;
                        break;
                    }
                }
            }
            return consoleApp;
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

            if (commandType == null && argsType != null)
            {
                throw new NotImplementedException(
                    string.Format("No class with an Execute-method with the class '{0}' as argument could be found", argsType.Name));
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

        private void ExecuteCommand(Type commandType, CommandSet command)
        {
            var methodInfo = commandType.GetMethod(ExecuteMethod);
            try
            {
                var result = methodInfo.Invoke(command.Command, new[] { command.Args });
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
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