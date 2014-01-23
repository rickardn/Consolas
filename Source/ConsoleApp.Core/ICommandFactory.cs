using System;

namespace Consolas.Core
{
    public interface ICommandFactory
    {
        object CreateInstance(Type commandType);
    }
}