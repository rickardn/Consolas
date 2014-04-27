namespace Consolas.Core
{
    public abstract class Command
    {
        public void Render<T>(string viewName, T model)
        {
            var viewEngine = ViewEngines.CreateEngine(this, viewName);
            viewEngine.Render(viewName, model);
        }

        public IViewEngineFactory ViewEngines { get; set; }
        
        public void Render(string viewName)
        {
            Render<object>(viewName, null);
        }
    }
}