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
            var view = ViewEngines.FindView(this, viewName);

            string result = view.Render(model);
            Console.WriteLine(result);
        }
    }
}