using System;

namespace Consolas.Core.Tests.Helpers
{
    public class CommandThrowsException
    {
        public string Execute(ArgThrowsException args)
        {
            throw new Exception("failure");
        }
    }
}