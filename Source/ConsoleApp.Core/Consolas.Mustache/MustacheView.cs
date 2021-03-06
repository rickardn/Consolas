﻿using Consolas.Core;

namespace Consolas.Mustache
{
    public class MustacheView : IView
    {
        private readonly string _view;

        public MustacheView(string view)
        {
            _view = view;
        }

        public string Render<T>(T model)
        {
            return Nustache.Core.Render.StringToString(_view, model);
        }
    }
}