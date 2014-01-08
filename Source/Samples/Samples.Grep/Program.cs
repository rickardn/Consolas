using ConsoleApp.Core;
using SimpleInjector;

namespace Samples.Grep
{
    public class Program : ConsoleAppBase
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
