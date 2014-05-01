using System.IO;
using System.Reflection;

namespace Consolas.Core
{
    public abstract class ManifestResourcePathViewEngine : IViewEngine
    {
        public string[] FileExtensions { get; set; }

        public IView FindView(Command command, string viewName)
        {
            var assembly = command.GetType().Assembly;
            var text = GetView(viewName, assembly);
            return text != null ? CreateView(text) : null;
        }

        protected abstract IView CreateView(string text);

        protected virtual string GetView(string viewName, Assembly assembly)
        {
            return GetManifestResouceView(viewName, assembly) 
                ?? GetFileView(viewName, assembly);
        }

        private string GetFileView(string viewName, Assembly assembly)
        {
            var assemblyDirectory = new FileInfo(assembly.Location).Directory;

            if (assemblyDirectory != null)
            {
                var viewsPath = Path.Combine(assemblyDirectory.FullName, "Views");

                foreach (var fileExtension in FileExtensions)
                {
                    var location = string.Format("{0}\\{1}{2}", viewsPath, viewName, fileExtension);

                    if (File.Exists(location))
                    {
                        {
                            return File.ReadAllText(location);
                        }
                    }
                }
            }
            return null;
        }

        private string GetManifestResouceView(string viewName, Assembly assembly)
        {
            var names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                foreach (var fileExtension in FileExtensions)
                {
                    if (name.EndsWith(string.Format("Views.{0}{1}", viewName, fileExtension)))
                    {
                        var manifestResourceStream = assembly.GetManifestResourceStream(name);
                        if (manifestResourceStream != null)
                        {
                            return new StreamReader(manifestResourceStream).ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }
    }
}