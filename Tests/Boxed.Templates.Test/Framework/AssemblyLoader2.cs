namespace Boxed.Templates.Test
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Loader;
    using Microsoft.Extensions.DependencyModel;

    public class AssemblyLoader2 : AssemblyLoadContext
    {
        private readonly string directoryPath;

        public AssemblyLoader2(string directoryPath) =>
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
                else
                {
                    var dotnetSdkDirectoryPath = @"C:\Program Files\dotnet\store\x64\netcoreapp2.0";
                    var runtimeStoreDirectoryPath = @"C:\Program Files\dotnet\store\x64\netcoreapp2.0";
                    var assemblyFileName = $"{assemblyName.Name}.dll";
                    var dotnetFiles = Directory.GetFiles(dotnetSdkDirectoryPath, assemblyFileName, SearchOption.AllDirectories);
                    var sdkFiles = Directory.GetFiles(runtimeStoreDirectoryPath, assemblyFileName, SearchOption.AllDirectories);
                    var dotnetAssemblyFilePath = dotnetFiles.Concat(sdkFiles).FirstOrDefault();
                    if (dotnetAssemblyFilePath != null)
                    {
                        return this.LoadFromAssemblyPath(dotnetAssemblyFilePath);
                    }
                }
            }

            return Assembly.Load(assemblyName);
        }
    }
}
