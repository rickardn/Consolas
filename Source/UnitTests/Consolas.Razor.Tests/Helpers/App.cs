using Consolas.Core;
using SimpleInjector;

namespace Consolas.Razor.Tests.Helpers
{
    public class App : ConsoleApp<App>
    {
        public void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            ViewEngines.Add<RazorViewEngine>();
        }
    }
}