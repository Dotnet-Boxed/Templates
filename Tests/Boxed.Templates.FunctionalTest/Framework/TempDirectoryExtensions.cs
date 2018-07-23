namespace Boxed.Templates.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;

    public static class TempDirectoryExtensions
    {
        public static async Task<Project> DotnetNew(
            this TempDirectory tempDirectory,
            string templateName,
            string name,
            IDictionary<string, string> arguments = null,
            TimeSpan? timeout = null)
        {
            var stringBuilder = new StringBuilder($"new {templateName} --name \"{name}\"");
            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    stringBuilder.Append($" --{argument.Key} \"{argument.Value}\"");
                }
            }

            await ProcessAssert.AssertStart(
                tempDirectory.DirectoryPath,
                "dotnet",
                stringBuilder.ToString(),
                CancellationTokenFactory.GetCancellationToken(timeout));

            var projectDirectoryPath = Path.Combine(tempDirectory.DirectoryPath, name);
            var projectFilePath = Path.Combine(projectDirectoryPath, name + ".csproj");
            var publishDirectoryPath = Path.Combine(projectDirectoryPath, "Publish");
            return new Project(name, projectFilePath, projectDirectoryPath, publishDirectoryPath);
        }
    }
}
