using System.IO;
using System.Reflection;

namespace Consolas.Core
{
    public abstract class ManifestResourcePathViewEngine : IViewEngine
    {
        public string[] FileExtensions { get; set; }

        public IView FindView(CommandContext commandContext, string viewName)
        {
            var text = GetView(viewName, commandContext.Assembly);
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
            string fileView = null;
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
                            fileView = File.ReadAllText(location);
                        }
                    }
                }
            }
            return fileView;
        }

        private string GetManifestResouceView(string viewName, Assembly assembly)
        {
            string resourceView = null;
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
                            resourceView = new StreamReader(manifestResourceStream).ReadToEnd();
                        }
                    }
                }
            }
            return resourceView;
        }
    }
}