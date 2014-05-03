using System;
using Consolas.Core;

namespace Consolas.Mustache
{
    public class MustacheView : IView
    {
        private readonly string _view;

        public MustacheView(string view)
        {
            _view = view;
        }

        public void Render<T>(T model)
        {
            Console.WriteLine(Nustache.Core.Render.StringToString(_view, model));
        }
    }
}