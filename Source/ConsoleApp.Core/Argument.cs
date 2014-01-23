namespace Consolas.Core
{
    public class Argument
    {
        public static Argument NoMatch = new Argument();
        public bool IsMatch { get; set; }
        public bool IsDefault { get; set; }
        public string Value { get; set; }
    }
}