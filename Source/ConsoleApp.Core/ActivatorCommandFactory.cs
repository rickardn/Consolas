using System;

namespace ConsoleApp.Core
{
    public class ActivatorCommandFactory : ICommandFactory
    {
        public object CreateInstance(Type commandType)
        {
            return Activator.CreateInstance(commandType);
        }
    }
}