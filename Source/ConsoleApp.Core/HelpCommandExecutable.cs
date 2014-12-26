using System;
using System.Reflection;

namespace Consolas.Core
{
    public class HelpCommandExecutable : IExecutable
    {
        private readonly Assembly _assembly;

        public HelpCommandExecutable(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Execute()
        {
            Console.WriteLine("Using: {0}.exe ...", _assembly.GetName().Name.ToLower());
        }
    }
}