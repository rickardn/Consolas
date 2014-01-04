namespace ConsoleApp.Core.Tests.Helpers
{
    public class SimpleConsoleApp : ConsoleAppBase
    {
        public void Main(string[] args)
        {
            Match(args);
        }
    }

    public class ConsoleAppWithNullDefaultCommand : ConsoleAppBase
    {
        public void Main(string[] args)
        {
            Match(args, null);
        }
    }

    public class ConsoleAppWithDefaultCommand : ConsoleAppBase
    {
         public void Main(string[] args)
         {
             Match(args, typeof(SingleParameter));
         }
    }
}