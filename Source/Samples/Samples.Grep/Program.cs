using System;
using System.Diagnostics;
using ConsoleApp.Core;
using SimpleInjector;

namespace Samples.Grep
{
    public class Program : ConsoleApp.Core.ConsoleApp
    {
        public static void Main(string[] args)
        {
            var watch = new Stopwatch();
            watch.Start();
            Match(args);
            watch.Stop();
            Console.WriteLine(watch.Elapsed.Seconds + "," + watch.ElapsedMilliseconds);
        }

        public override void Configure(Container container)
        {
            container.Register<IConsole,SystemConsole>();
        }
    }
}
