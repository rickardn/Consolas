using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace ConsoleApp.Core
{
    public abstract class ConsoleAppBase
    {
        private static Action _defaultCommand;
        internal static Container Container;
        private static Type _defaultCommandArgs;
        private static Type[] _argTypes;
        private static ArgumentMatcher _matcher;
        private static string[] _args;

        protected static Action DefaultCommand
        {
            set { _defaultCommand = value; }
        }

        public virtual void Configure(Container container)
        {
        }

        protected static void Match(string[] args, Type defaultCommandArgs)
        {
            if (defaultCommandArgs != null)
            {
                _defaultCommandArgs = defaultCommandArgs;
            }

            // Do not refactor
            _argTypes = Assembly.GetCallingAssembly().GetTypes();

            MatchArgs(args);
        }

        protected static void Match(string[] args)
        {
            // Do not refactor
            _argTypes = Assembly.GetCallingAssembly().GetTypes();

            MatchArgs(args);
        }

        private static void MatchArgs(string[] args)
        {
            CreateAndConfigureConsoleApp();

            _args = args;

            _matcher = new ArgumentMatcher
            {
                Types = _argTypes.ToList()
            };

            Type argsType = _matcher.Match(_args);

            Type commandType = FindCommandType(argsType);

            if (commandType != null)
            {
                ExecuteCommand(commandType, argsType);
            }
            else
            {
                ExecuteDefaultCommand();
            }
        }

        private static void CreateAndConfigureConsoleApp()
        {
            var stack = new StackTrace();
            var stackFrames = stack.GetFrames();
            if (stackFrames == null)
            {
                return;
            }

            foreach (StackFrame frame in stackFrames)
            {
                var method = frame.GetMethod();
                if (method.Name != "Match")
                {
                    var declaringType = frame.GetMethod().DeclaringType;
                    if (declaringType != null && declaringType.IsAbstract)
                        continue;

                    if (declaringType != null)
                    {
                        var o = Activator.CreateInstance(declaringType) as ConsoleAppBase;
                        if (o != null)
                        {
                            Container = new Container();
                            CommandBuilder.Current.SetCommandFactory(
                                new SimpleInjectorCommandFactory(Container));
                            o.Configure(Container);
                        }
                    }
                    break;
                }
            }
        }

        private static void ExecuteCommand(Type commandType, Type argsType)
        {
            var command = CommandBuilder.Current.GetCommandInstance(commandType);
            var arg = _matcher.MatchToObject(_args, argsType);

            var methodInfo = commandType.GetMethod("Execute");
            var result = methodInfo.Invoke(command, new[] {arg});
            Console.WriteLine(result);
        }

        private static Type FindCommandType(Type argsType)
        {
            var commands = _argTypes
                .Where(x => x.GetMethods().Any(
                    m => m.GetParameters().Any(
                        p => p.ParameterType == argsType)));

            Type commandType = commands.FirstOrDefault();
            return commandType;
        }

        private static void ExecuteDefaultCommand()
        {
            if (_defaultCommand == null)
            {
                if (_defaultCommandArgs == null)
                {
                    throw new ArgumentException();
                }
                else
                {
                    var commandType = FindCommandType(_defaultCommandArgs);
                    if (commandType != null)
                    {
                        ExecuteCommand(commandType, _defaultCommandArgs);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
            }
            else
            {
                _defaultCommand();
            }
        }
    }
}