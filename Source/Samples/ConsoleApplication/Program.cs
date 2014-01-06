using System;
using ConsoleApp.Core;
using SimpleInjector;

namespace SampleConsoleApplication
{
    public class Program : ConsoleAppBase
    {
        public static void Main(string[] args)
        {
            //DefaultCommand = () => Console.WriteLine("foo");
            Match(args);
        }

        public override void Configure(Container container)
        {
            container.Register<IService, MyService>();
        }
    }

    public class MyService : IService
    {
    }

    public interface IService
    {
    }
}
