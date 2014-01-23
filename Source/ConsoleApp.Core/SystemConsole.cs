using System;

namespace Consolas.Core
{
    public class SystemConsole : IConsole
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}