using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
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
        ///     Overide to configure your console application and register dependencies with the container.
        /// </summary>
        /// <param name="container"></param>
        public virtual void Configure(Container container) {}

        /// <summary>
        ///     Call <see cref="Match"/> in your Main method to start matching program arguments to commands in the application.
        /// </summary>
        /// <param name="args"></param>
        protected static void Match(string[] args)
        {
            ConsoleApp app = CreateConsoleApp();
            Configure(app);

            IExecutable executable = app.FindExecutable(args);
            if (executable != null)
            {
                executable.Execute();
            }
        }

        private IExecutable FindExecutable(string[] args)
        {
            CommandType commandType = FindCommandType(args);

            if (commandType != null)
            {
                return new CommandExecutable(args, commandType, _argumentMatcher);
            }
            
            var defaultView = TryFindDefaultView();
            return defaultView 
                ?? new HelpCommandExecutable(GetType().Assembly);
        }

        private IExecutable TryFindDefaultView()
        {
            try
            {
                var context = new CommandContext(GetType().Assembly);
                IView view = ViewEngines.FindView(context, DefaultViewName);
                return new ViewExecutable(view);
            }
            catch (ViewEngineException e)
            {
                Console.WriteLine(e);
            }
            return null;
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
            var stack = new StackTrace();
            var stackFrames = stack.GetFrames();

            var builder = new StringBuilder();


            // ReSharper disable PossibleNullReferenceException
            foreach (StackFrame frame in stackFrames)
            // ReSharper restore PossibleNullReferenceException
            {
                var method = frame.GetMethod();

                builder.AppendLine(frame.ToString());
                //method.DeclaringType.Name + "." + method.Name);


                if (method.Name == InitMethodName) continue;

                Type declaringType = method.DeclaringType;
                if (declaringType == null) continue;
                if (declaringType.IsAbstract) continue;

                ConsoleApp consoleApp = null;

                try
                {
                    consoleApp = Activator.CreateInstance(declaringType) as ConsoleApp;
                }
                catch (MissingMethodException)
                {
                    // ignore
                }

                if (consoleApp == null) continue;

                return consoleApp;
            }

            throw new Exception("Unable to create Console App instance \n" + builder.ToString());
        }

        private CommandType FindCommandType(string[] args)
        {
            Assembly appAssembly = GetType().Assembly;
            List<Type> argTypes = Arguments.Count > 0 
                ? Arguments
                : appAssembly.GetTypes().Where(t => t.Name.EndsWith("Args")).ToList();
            _argumentMatcher = new ArgumentMatcher
            {
                Types = argTypes
            };

            var allTypes = appAssembly.GetTypes().ToList();

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

        private Type FindMatchingCommandType(IEnumerable<Type> argTypes, Type argsType)
        {
            return
                argTypes.FirstOrDefault(
                    x => x.GetMethods().Any(m => m.GetParameters().Any(p => p.ParameterType == argsType)));
        }
    }
}