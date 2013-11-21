namespace ConsoleApp.Core.Tests.Helpers
{
    public class MyConsoleAppBase : ConsoleAppBase
    {
        public void Main(string[] args)
        {
            Match(args);
        }
    }
}