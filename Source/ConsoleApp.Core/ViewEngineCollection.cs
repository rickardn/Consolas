using System.Collections.Generic;
using System.Linq;
using SimpleInjector;

namespace Consolas.Core
{
    public class ViewEngineCollection : List<IViewEngine>
    {
        private readonly Container _container;

        public ViewEngineCollection(Container container)
        {
            _container = container;
        }

        /// <summary>
        ///     Adds a view engine to the end of the collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Add<T>() where T : class, IViewEngine
        {
            var viewEngine = _container.GetInstance<T>();
            Add(viewEngine);
        }

        public IView FindView(Command command, string viewName)
        {
            return this.Select(viewEngine => viewEngine.FindView(command, viewName))
                .FirstOrDefault(view => view != null);
        }
    }
}