using Consolas.Core;
using Consolas.Razor;
using Samples.Ping.Models;
using SimpleInjector;

namespace Samples.Ping
{
    public class Program : ConsoleApp
    {
        public static void Main(string[] args)
        {
            Match(args);
        }

        public override void Configure(Container container)
        {
            container.Register<IConsole, SystemConsole>();
            container.Register<IThreadService, ThreadService>();
            
            ViewEngines.Add<RazorViewEngine>();
        }
    }
}