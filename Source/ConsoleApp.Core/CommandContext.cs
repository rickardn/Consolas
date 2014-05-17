using System;
using System.Reflection;

namespace Consolas.Core
{
    public class CommandContext
    {
        public Command Command { get; set; }

        public Assembly Assembly { get; set; }

        public CommandContext() {}

        public CommandContext(Command command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            Command = command;
            Assembly = command.GetType().Assembly;
        }

        public CommandContext(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            Assembly = assembly;
        }
    }
}