using System;

namespace Consolas.Core
{
    public interface IViewEngine
    {
        IView FindView(Command command, string viewName);
    }

    public interface IView
    {
        void Render<T>(T model);
    }
}