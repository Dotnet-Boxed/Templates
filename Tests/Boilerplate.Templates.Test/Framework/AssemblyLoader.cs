namespace Boilerplate.Templates.Test
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using Microsoft.Extensions.DependencyModel;

    public class AssemblyLoader : AssemblyLoadContext
    {
        private readonly string directoryPath;

        public AssemblyLoader(string directoryPath) =>
            this.directoryPath = directoryPath;

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var dependencyContext = DependencyContext.Default;
            var compilationLibraries = dependencyContext
                .CompileLibraries
                .Where(x => x.Name.Contains(assemblyName.Name))
                .ToList();
            if (compilationLibraries.Count > 0)
            {
                return Assembly.Load(new AssemblyName(compilationLibraries.First().Name));
            }
            else
            {
                var assemblyFilePath = Path.Combine(this.directoryPath, $"{assemblyName.Name}.dll");
                if (File.Exists(assemblyFilePath))
                {
                    return this.LoadFromAssemblyPath(assemblyFilePath);
                }
            }

            return Assembly.Load(assemblyName);
        }
    }
}
