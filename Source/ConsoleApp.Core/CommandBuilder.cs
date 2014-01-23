using System;

namespace Consolas.Core
{
    public class CommandBuilder
    {
        public static CommandBuilder Current = new CommandBuilder();
        private ICommandFactory _factory;

        private CommandBuilder()
        {
            SetCommandFactory(new ActivatorCommandFactory());
        }

        public void SetCommandFactory(ICommandFactory factory)
        {
            _factory = factory;
        }

        public object GetCommandInstance(Type commandType)
        {
            return _factory.CreateInstance(commandType);
        }
    }
}