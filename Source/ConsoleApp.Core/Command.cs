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

            string result = view.Render(model);
            Console.WriteLine(result);
        }

        private IView FindView<T>(string viewName)
        {
            if (ViewEngines == null || ViewEngines.Count == 0)
            {
                var message = string.Format("No view engines have been added, call ViewEngines.Add<>() in your Program.cs");
                throw new ViewEngineException(message);
            }

            IView view = ViewEngines.FindView(this, viewName);

            if (view == null)
            {
                var message = string.Format("No view found with the name '{0}'\nMake sure you set the view's Build action to 'Embedded resource'", viewName);
                throw new ViewEngineException(message);
            }

            return view;
        }
    }
}