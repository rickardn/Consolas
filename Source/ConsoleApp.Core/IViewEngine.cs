namespace Consolas.Core
{
    public interface IViewEngine
    {
        IView FindView(Command command, string viewName);
    }
}