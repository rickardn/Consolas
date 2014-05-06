using Consolas.Core;

namespace Consolas.Razor
{
    public class RazorView : IView
    {
        private readonly string _view;

        public RazorView(string view)
        {
            _view = view;
        }

        public string Render<T>(T model)
        {
            return RazorEngine.Razor.Parse(_view, model);
        }
    }
}