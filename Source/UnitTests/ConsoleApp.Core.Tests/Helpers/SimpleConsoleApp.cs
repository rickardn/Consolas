using SimpleInjector;

namespace Consolas.Core.Tests.Helpers
{
    public class SimpleConsoleApp : ConsoleApp
    {
        public void Main(string[] args)
        {
            Match(args);
        }

        public Container DependencyContainer
        {
            get { return Container; }
        }
    }
}

