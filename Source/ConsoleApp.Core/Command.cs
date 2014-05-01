namespace Consolas.Core
{
    public abstract class Command
    {
        public ViewEngineCollection ViewEngines { get; set; }

        public void Render<T>(string viewName, T model)
        {
            if (ViewEngines != null)
            {
                var view = ViewEngines.FindView(this, viewName);
                if (view != null)
                {
                    view.Render(model);
                }
            }
        }

        public void Render(string viewName)
        {
            Render<object>(viewName, null);
        }
    }
}