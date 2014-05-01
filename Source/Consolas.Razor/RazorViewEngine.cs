using Consolas.Core;

namespace Consolas.Razor
{
    public class RazorViewEngine : ManifestResourcePathViewEngine
    {
        public RazorViewEngine()
        {
            FileExtensions = new[] {".cstxt"};
        }

        protected override IView CreateView(string text)
        {
            return new RazorView(text);
        }
    }
}