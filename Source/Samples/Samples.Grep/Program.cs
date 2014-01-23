using Consolas.Core;
using SimpleInjector;

namespace Samples.Grep
{
    public class Program : Consolas.Core.ConsoleApp
    {
        public static void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            container.Register<IConsole,SystemConsole>();
        }
    }
}
