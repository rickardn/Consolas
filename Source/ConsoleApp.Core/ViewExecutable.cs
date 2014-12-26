using System;

namespace Consolas.Core
{
    public class ViewExecutable : IExecutable
    {
        private readonly IView _view;

        public ViewExecutable(IView view)
        {
            _view = view;
        }

        public void Execute()
        {
            string result = _view.Render<object>(null);
            Console.WriteLine(result);
        }
    }
}