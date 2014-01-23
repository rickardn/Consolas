using System;
using SimpleInjector;

namespace Consolas.Core
{
    public class SimpleInjectorCommandFactory : ICommandFactory
    {
        private readonly Container _container;

        public SimpleInjectorCommandFactory(Container container)
        {
            _container = container;
        }

        public object CreateInstance(Type commandType)
        {
            return _container.GetInstance(commandType);
        }
    }
}