namespace Consolas.Core
{
    public interface IViewEngineFactory
    {
        IViewEngine CreateEngine(string viewName);
    }
}