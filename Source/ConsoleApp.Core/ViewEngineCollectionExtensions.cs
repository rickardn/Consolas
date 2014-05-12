using System.Linq;

namespace Consolas.Core
{
    public static class ViewEngineCollectionExtensions
    {
        public static IView FindView(this ViewEngineCollection viewEngines, Command command, string viewName)
        {
            if (viewEngines == null || viewEngines.Count == 0)
            {
                var message =
                    string.Format("No view engines have been added, call ViewEngines.Add<>() in your Program.cs");
                throw new ViewEngineException(message);
            }

            IView result = 
                (from viewEngine in viewEngines
                 select viewEngine.FindView(command, viewName))
                .FirstOrDefault(view => view != null);

            if (result == null)
            {
                var message =
                    string.Format(
                        "No view found with the name '{0}'\nMake sure you set the view's Build action to 'Embedded resource'",
                        viewName);
                throw new ViewEngineException(message);
            }

            return result;
        }
    }
}