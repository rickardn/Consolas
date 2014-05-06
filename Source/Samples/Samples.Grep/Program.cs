using Consolas.Core;
using Consolas.Mustache;
using SimpleInjector;

namespace Samples.Grep
{
    public class Program : ConsoleApp
    {
        public static void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            container.Register<IConsole,SystemConsole>();
            ViewEngines.Add<MustacheViewEngine>();
        }
    }
}
