using System;
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

        public void Render<T>(T model)
        {
            Console.WriteLine(RazorEngine.Razor.Parse(_view, model));
        }
    }
}