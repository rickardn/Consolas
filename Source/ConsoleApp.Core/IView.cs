namespace Consolas.Core
{
    public interface IView
    {
        string Render<T>(T model);
    }
}