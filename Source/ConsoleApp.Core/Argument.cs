namespace ConsoleApp.Core
{
    public class Argument
    {
        public static Argument NoMatch = new Argument();
        public bool IsMatch { get; set; }
        public string Value { get; set; }
    }
}