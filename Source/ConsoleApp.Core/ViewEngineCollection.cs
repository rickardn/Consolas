using System;
using System.Collections.Generic;
using System.Linq;

namespace Consolas.Core
{
    public class ViewEngineCollection : List<IViewEngine>
    {
        public void Add<T>() where T : class, IViewEngine
        {
            var viewEngine = Activator.CreateInstance<T>();
            Add(viewEngine);
        }

        public IView FindView(Command command, string viewName)
        {
            return this.Select(viewEngine => viewEngine.FindView(command, viewName))
                .FirstOrDefault(view => view != null);
        }
    }
}