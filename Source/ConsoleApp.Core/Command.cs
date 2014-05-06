using System;

namespace Consolas.Core
{
    public abstract class Command
    {
        public ViewEngineCollection ViewEngines { get; set; }

        protected object View(string viewName)
        {
            return View<object>(viewName, null);
        }

        protected object View<T>(string viewName, T model)
        {
            return new CommandResult
            {
                Model = model,
                ViewName = viewName
            };
        }

        protected void Render(string viewName)
        {
            Render<object>(viewName, null);
        }

        protected void Render<T>(string viewName, T model)
        {
            var view = FindView<T>(viewName);
            string result;

            if (view != null)
            {
                result = view.Render(model);
            }
            else
            {
                throw new ViewEngineException("No view found for " + viewName);
            }

            Console.WriteLine(result);
        }


        private IView FindView<T>(string viewName)
        {
            IView view = null;

            if (ViewEngines != null)
            {
                view = ViewEngines.FindView(this, viewName);
            }
            return view;
        }
    }
}