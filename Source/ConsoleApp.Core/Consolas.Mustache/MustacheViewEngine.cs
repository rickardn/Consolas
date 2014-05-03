using Consolas.Core;

namespace Consolas.Mustache
{
    public class MustacheViewEngine : ManifestResourcePathViewEngine
    {
        public MustacheViewEngine()
        {
            FileExtensions = new[]
            {
                ".template"
            };
        }

        protected override IView CreateView(string text)
        {
            return new MustacheView(text);
        }
    }
}