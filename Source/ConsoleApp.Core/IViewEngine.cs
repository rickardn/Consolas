namespace Consolas.Core
{
    public interface IViewEngine
    {
        void Render<T>(string viewName, T model);
    }
}