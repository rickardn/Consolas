using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Consolas.Mustache;
using SimpleInjector;

namespace Consolas.Core
{
    /// <summary>
    ///     Defines the methods and properties that are common to all application objects in a Console application. This class
    ///     is the base class for applications that are defined by the user in a Program.cs file
    /// </summary>
    public abstract class ConsoleApp
    {
        private const string InitMethodName = "Match";
        private const string ExecuteMethod = "Execute";
        private const string DefaultViewName = "Default";
        private ArgumentMatcher _argumentMatcher;
        private static Container _container;

        /// <summary>
        ///     A collection of argument types. Use this property to add new types which should be 
        ///     used to match against.
        /// </summary>
        public ArgumentTypeCollection Arguments { get; set; }

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
                CommandSet commandSet = app.CreateCommandWithArgs(args, commandType);
                app.ExecuteCommand(commandType.Command, commandSet);
            }
            else if (!TryRenderDefaultView(callingAssembly, app))
            {
                Console.WriteLine("Using: {0}.exe ...", callingAssembly.GetName().Name.ToLower());
            }
        }

        private static bool TryRenderDefaultView(Assembly callingAssembly, ConsoleApp app)
        {
            try
            {
                var context = new CommandContext(callingAssembly);
                var view = app.ViewEngines.FindView(context, DefaultViewName);
                var result = view.Render<object>(null);
                Console.WriteLine(result);
                return true;
            }
            catch (ViewEngineException)
            {
            }
            return false;
        }

        private static void Configure(ConsoleApp app)
        {
            _container = new Container();
            _container.Options.AllowOverridingRegistrations = true;

            app.Arguments = app.Arguments ?? new ArgumentTypeCollection();
            app.ViewEngines = app.ViewEngines ?? new ViewEngineCollection(_container);

            _container.RegisterInitializer<Command>(command =>
            {
                command.ViewEngines = app.ViewEngines;
            });


            CommandBuilder.Current.SetCommandFactory(new SimpleInjectorCommandFactory(_container));
            
            app.ViewEngines.Add(new MustacheViewEngine());
            
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
            List<Type> argTypes = Arguments.Count > 0 
                ? Arguments
                : callingAssembly.GetTypes().Where(t => t.Name.EndsWith("Args")).ToList();
            _argumentMatcher = new ArgumentMatcher
            {
                Types = argTypes
            };

            var allTypes = callingAssembly.GetTypes().ToList();

            Type argsType = null;
            try
            {
                argsType = _argumentMatcher.Match(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Type commandType = FindMatchingCommandType(allTypes, argsType);

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

        private CommandSet CreateCommandWithArgs(string[] args, CommandType commandType)
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
            var executeMethod = FindExecuteMethod(commandType, command);
            var result = TryInvokeCommand(command, executeMethod);
            result = HandleCommandResult(command, result);

            Console.WriteLine(result);
        }

        private static MethodInfo FindExecuteMethod(Type commandType, CommandSet command)
        {
            MethodInfo executeMethodInfo = null;

            IEnumerable<MethodInfo> methods =
                from method in commandType.GetMethods()
                where method.Name == ExecuteMethod
                select method;

            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                foreach (var parameter in parameters)
                {
                    if (parameter.ParameterType == command.Args.GetType())
                    {
                        executeMethodInfo = method;
                        break;
                    }
                }
            }
            return executeMethodInfo;
        }

        private Type FindMatchingCommandType(IEnumerable<Type> argTypes, Type argsType)
        {
            return
                argTypes.FirstOrDefault(
                    x => x.GetMethods().Any(m => m.GetParameters().Any(p => p.ParameterType == argsType)));
        }

        private static object TryInvokeCommand(CommandSet command, MethodInfo methodInfo)
        {
            object result;
            try
            {
                result = methodInfo.Invoke(command.Command, new[] {command.Args});
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
            return result;
        }

        private object HandleCommandResult(CommandSet commandSet, object result)
        {
            var commandResult = result as CommandResult;
            if (commandResult != null)
            {
                var command = (Command) commandSet.Command;
                var view = ViewEngines.FindView(command.Context, commandResult.ViewName);
                result = view.Render(commandResult.Model);
            }
            return result;
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