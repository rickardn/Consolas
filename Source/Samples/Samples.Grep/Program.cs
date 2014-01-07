using ConsoleApp.Core;
using SimpleInjector;

namespace Samples.Grep
{
    class Program : ConsoleAppBase
    {
        static void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            container.Register<IConsole,SystemConsole>();
        }
    }
}
