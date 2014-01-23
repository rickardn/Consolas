using System;

namespace Consolas.Core
{
    public class ActivatorCommandFactory : ICommandFactory
    {
        public object CreateInstance(Type commandType)
        {
            return Activator.CreateInstance(commandType);
        }
    }
}