using System;

namespace ConsoleApp.Core
{
    public interface ICommandFactory
    {
        object CreateInstance(Type commandType);
    }
}