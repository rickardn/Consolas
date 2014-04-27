using System;
using System.IO;
using System.Reflection;
using RazorEngine;

namespace Consolas.Core
{
    public class RazorViewEngine : IViewEngine
    {
        private readonly Command _command;

        public RazorViewEngine(Command command)
        {
            _command = command;
        }

        public void Render<T>(string viewName, T model)
        {
            var assembly = _command.GetType().Assembly;
            var text = GetView(viewName, assembly);
            var result = Razor.Parse(text, model);
            Console.Write(result);
        }

        private string GetView(string viewName, Assembly assembly)
        {
            var names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                if (name.Contains(string.Format("Views.{0}.cstxt", viewName)))
                {
                    var reader = new StreamReader(assembly.GetManifestResourceStream(name));
                    return reader.ReadToEnd();
                }
            }

            var assemblyDirectory = new FileInfo(assembly.Location).Directory;

            var viewsPath = Path.Combine(assemblyDirectory.FullName, "Views");

            var location = string.Format("{0}\\{1}.cstxt", viewsPath, viewName);

            if (File.Exists(location))
            {
                return File.ReadAllText(location);
            }

            return null;
        }
    }
}