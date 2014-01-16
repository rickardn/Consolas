using System;
using System.IO;
using RazorEngine;

namespace ConsoleApp.Core
{
    public abstract class Command
    {
        public void Render<T>(string viewName, T model)
        {
            var text = GetView(viewName);
            var result = Razor.Parse(text, model);
            Console.Write(result);
        }

        private string GetView(string viewName)
        {
            var assembly = GetType().Assembly;

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