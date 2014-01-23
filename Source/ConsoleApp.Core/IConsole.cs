namespace Consolas.Core
{
    public interface IConsole
    {
        void Write(string value);
        void WriteLine(string value);
        void WriteLine(object value);
    }
}