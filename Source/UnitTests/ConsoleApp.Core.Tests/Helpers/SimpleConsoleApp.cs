using SimpleInjector;

namespace Consolas.Core.Tests.Helpers
{
    public class SimpleConsoleApp : ConsoleApp
    {
        public void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            base.Configure(container);
            ViewEngines.Clear();
        }
    }
}

