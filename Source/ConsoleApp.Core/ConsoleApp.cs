using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace Consolas.Core
{
    /// <summary>
    ///     Defines the methods and properties that are common to all application objects in a Console application. This class
    ///     is the base class for applications that are defined by the user in a Program.cs file
    /// </summary>
    public abstract class ConsoleApp
    {
        /// <summary>
        ///     A collection of view engines. Use this property to add new view engines
        ///     to the application.
        /// </summary>
        public ViewEngineCollection ViewEngines { get; set; }

        /// <summary>
        ///     Overide to configure your console application and register dependencies.
        /// </summary>
        /// <param name="container"></param>
        public virtual void Configure(Container container) {}

        /// <summary>
        ///     Call <see cref="Match"/> in your Main method to start matching program arguments to commands in the application.
        /// </summary>
        /// <param name="args"></param>
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

        private const string InitMethodName = "Match";

        private const string ExecuteMethod = "Execute";

        private ArgumentMatcher _argumentMatcher;

        private static Container _container;

        private static void Configure(ConsoleApp app)
        {
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;

            app.ViewEngines = new ViewEngineCollection(_container);
            _container.RegisterInitializer<Command>(command => { command.ViewEngines = app.ViewEngines; });

            CommandBuilder.Current.SetCommandFactory(new SimpleInjectorCommandFactory(_container));
            app.Configure(_container);
            _container.Verify();
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

                    if (declaringType == null) continue;
                    
                    consoleApp = Activator.CreateInstance(declaringType) as ConsoleApp;
                    break;
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
                    string.Format("No class with an Execute-method with the class '{0}' as argument could be found",
                        argsType.Name));
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
                var result = methodInfo.Invoke(command.Command, new[] {command.Args});
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        private Type FindMatchingCommandType(IEnumerable<Type> argTypes, Type argsType)
        {
            return
                argTypes.FirstOrDefault(
                    x => x.GetMethods().Any(m => m.GetParameters().Any(p => p.ParameterType == argsType)));
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