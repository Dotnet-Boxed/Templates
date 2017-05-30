namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;

    public class ProjectTemplateTester : IDisposable
    {
        private readonly string projectDirectoryPath;
        private readonly FeatureCollection features;
        private readonly IFileSystemService fileSystemService;
        private readonly string tempDirectoryPath;
        private readonly string npmCacheDirectoryPath;
        private readonly string npmModulesDirectoryPath;

        public ProjectTemplateTester(string projectFilePath, string tempDirectoryPath)
        {
            this.projectDirectoryPath = Path.GetDirectoryName(projectFilePath);
            this.tempDirectoryPath = Path.Combine(
                tempDirectoryPath,
                Path.GetFileNameWithoutExtension(projectFilePath) + "-" + Guid.NewGuid().ToString());
            var tempProjectFilePath = Path.Combine(this.tempDirectoryPath, Path.GetFileName(projectFilePath));

            this.npmCacheDirectoryPath = Path.Combine(tempDirectoryPath, "npm");
            this.npmModulesDirectoryPath = Path.Combine(this.npmCacheDirectoryPath, "node_modules");

            DirectoryExtended.Copy(this.projectDirectoryPath, this.tempDirectoryPath);

            var container = new ContainerBuilder()
                .RegisterServices(tempProjectFilePath)
                .RegisterFeatureSet(FeatureSet.Mvc6)
                .Build();
            this.features = new FeatureCollection(container.Resolve<IEnumerable<IFeature>>());
            this.fileSystemService = container.Resolve<IFileSystemService>();
        }

        public FeatureCollection Features => this.features;

        public async Task AddOrRemoveFeatures()
        {
            foreach (var feature in this.features)
            {
                await feature.AddOrRemoveFeature();
            }

            await this.fileSystemService.SaveAll();
        }

        public async Task AssertDotnetBuildSucceeded()
        {
            await ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", "restore", TimeSpan.FromSeconds(20));
            await Task.Delay(2000);
            await ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", "build", TimeSpan.FromSeconds(20));
        }

        public async Task AssertNpmInstallSucceeded()
        {
            await this.EnsureNpmCacheInitialized();
            DirectoryExtended.Copy(this.npmModulesDirectoryPath, Path.Combine(this.tempDirectoryPath, "node_modules"));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, GetNpmFilePath(), "install", TimeSpan.FromMinutes(5));
        }

        public async Task AssertGulpCleanBuildTestSucceeded()
        {
            var gulpFilePath = Path.Combine(this.tempDirectoryPath, @"node_modules\.bin\gulp.cmd");
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "clean", TimeSpan.FromSeconds(20));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "build", TimeSpan.FromSeconds(20));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "test", TimeSpan.FromSeconds(20));
        }

        //private async Task AssertSiteStartsAndResponds(params string[] urls)
        //{
        //    var assembly = Assembly.LoadFile(Path.Combine(this.tempDirectoryPath, @"bin\Boilerplate.AspNerCore.Sample.dll"));
        //    var startupType = assembly.ExportedTypes.FirstOrDefault(x => string.Equals(x.Name, "Startup"));
        //    using (TestServer server = new TestServer(new WebHostBuilder().UseStartup(startupType)))
        //    {
        //        foreach (var url in urls)
        //        {
        //            var response = await server.CreateRequest(url).SendAsync("GET");
        //            response.EnsureSuccessStatusCode();
        //        }
        //    }
        //}

        public void Dispose() =>
            DirectoryExtended.Delete(this.tempDirectoryPath);

        private static string GetNpmFilePath() =>
            Environment
                .GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine)
                .Split(';')
                .Where(x => x.Contains("nodejs") && Directory.Exists(x))
                .SelectMany(x => Directory
                    .GetFiles(x, "*", SearchOption.AllDirectories)
                    .Where(y => string.Equals(Path.GetFileName(y), "npm.cmd", StringComparison.OrdinalIgnoreCase)))
                .First();

        private async Task EnsureNpmCacheInitialized()
        {
            var packageJsonFilePath = Path.Combine(this.projectDirectoryPath, "package.json");

            if (!Directory.Exists(this.npmModulesDirectoryPath))
            {
                if (!Directory.Exists(this.npmCacheDirectoryPath))
                {
                    Directory.CreateDirectory(this.npmCacheDirectoryPath);
                }

                var npmCachePackageJsonFilePath = Path.Combine(this.npmCacheDirectoryPath, "package.json");
                File.Copy(packageJsonFilePath, npmCachePackageJsonFilePath, true);
                File.WriteAllLines(
                    npmCachePackageJsonFilePath,
                    File.ReadAllLines(npmCachePackageJsonFilePath).Where(x => !x.Contains("//")));

                await ProcessAssert.AssertStart(this.npmCacheDirectoryPath, GetNpmFilePath(), "install", TimeSpan.FromMinutes(10));
            }
        }
    }
}
