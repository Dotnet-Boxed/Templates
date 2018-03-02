namespace Boilerplate.Templates.Test
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class TempDirectoryExtensions
    {
        public static async Task<Project> DotnetNew(
            this TempDirectory tempDirectory,
            string templateName,
            string name = null,
            TimeSpan? timeout = null)
        {
            await ProcessAssert.AssertStart(tempDirectory.DirectoryPath, "dotnet", $"new {templateName} --name \"{name}\"", timeout ?? TimeSpan.FromSeconds(20));
            var projectDirectoryPath = Path.Combine(tempDirectory.DirectoryPath, name);
            var projectFilePath = Path.Combine(projectDirectoryPath, name + ".csproj");
            var publishDirectoryPath = Path.Combine(projectDirectoryPath, "Publish");
            return new Project(name, projectFilePath, projectDirectoryPath, publishDirectoryPath);
        }
    }
}
