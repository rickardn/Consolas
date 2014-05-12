using System.Collections.Generic;
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
    }
}