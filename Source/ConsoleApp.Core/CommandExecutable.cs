using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Consolas.Core
{
    public class CommandExecutable : IExecutable
    {
        private const string ExecuteMethod = "Execute";
        private readonly CommandType _commandType;
        private readonly ArgumentMatcher _argumentMatcher;
        private readonly CommandSet _commandSet;

        public CommandExecutable(string[] args, CommandType commandType, ArgumentMatcher argumentMatcher)
        {
            _commandType = commandType;
            _argumentMatcher = argumentMatcher;
            _commandSet = CreateCommandWithArgs(args, commandType);
        }

        public void Execute()
        {
            ExecuteCommand(_commandType.Command, _commandSet);
        }

        private void ExecuteCommand(Type commandType, CommandSet command)
        {
            MethodInfo executeMethod = FindExecuteMethod(commandType, command);
            object result = TryInvokeCommand(command, executeMethod);
            result = HandleCommandResult(command, result);

            Console.WriteLine(result);
        }

        private static object TryInvokeCommand(CommandSet command, MethodInfo methodInfo)
        {
            object result;
            try
            {
                result = methodInfo.Invoke(command.Command, new[] { command.Args });
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
                var command = (Command)commandSet.Command;
                var view = command.ViewEngines.FindView(command.Context, commandResult.ViewName);
                result = view.Render(commandResult.Model);
            }
            return result;
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
    }
}