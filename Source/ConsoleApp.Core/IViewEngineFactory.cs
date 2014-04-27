namespace Consolas.Core
{
    public interface IViewEngineFactory
    {
        IViewEngine CreateEngine(Command command, string viewName);
    }

    public class ViewEngineFactory : IViewEngineFactory
    {
        private readonly IDependencyResolver _dependencyResolver;

        public ViewEngineFactory(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public IViewEngine CreateEngine(Command command, string viewName)
        {
            return new RazorViewEngine(command);
        }
    }
}