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
            // We need to add all types in test project as potential arguments 
            // as most don't follow the naming convention by ending with 'Args'
            foreach (var type in GetType().Assembly.GetTypes())
            {
                Arguments.Add(type);
            }

            base.Configure(container);
            ViewEngines.Clear();
        }
    }
}

