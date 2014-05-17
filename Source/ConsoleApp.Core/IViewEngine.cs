namespace Consolas.Core
{
    public interface IViewEngine
    {
        IView FindView(CommandContext commandContext, string viewName);
    }
}