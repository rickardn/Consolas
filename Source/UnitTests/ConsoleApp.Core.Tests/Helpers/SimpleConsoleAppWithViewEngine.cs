using Consolas.Mustache;
using SimpleInjector;

namespace Consolas.Core.Tests.Helpers
{
    public class SimpleConsoleAppWithViewEngine : ConsoleApp
    {
        public void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            ViewEngines.Add<MustacheViewEngine>();
        }
    }
}