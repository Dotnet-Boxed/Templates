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

        public ProjectTemplateTester(string projectFilePath, string tempDirectoryPath)
        {
            this.projectDirectoryPath = Path.GetDirectoryName(projectFilePath);

            if (string.IsNullOrEmpty(tempDirectoryPath))
            {
                this.tempDirectoryPath = Path.Combine(
                    Path.GetTempPath(),
                    Path.GetFileNameWithoutExtension(projectFilePath) + "-" + Guid.NewGuid().ToString());
            }
            else
            {
                this.tempDirectoryPath = Path.Combine(
                    tempDirectoryPath,
                    Path.GetFileNameWithoutExtension(projectFilePath) + "-" + Guid.NewGuid().ToString());
            }

            DirectoryExtended.Copy(this.projectDirectoryPath, this.tempDirectoryPath);

            var container = new ContainerBuilder()
                .RegisterServices(Path.Combine(this.tempDirectoryPath, "Boilerplate.AspNetCore.Sample.xproj"))
                .RegisterFeatureSet(FeatureSet.Mvc6)
                .Build();
            this.features = new FeatureCollection(container.Resolve<IEnumerable<IFeature>>());
            this.fileSystemService = container.Resolve<IFileSystemService>();
        }

        public FeatureCollection Features
        {
            get { return this.features; }
        }

        public async Task AddOrRemoveFeatures()
        {
            foreach (IFeature feature in this.features)
            {
                await feature.AddOrRemoveFeature();
            }

            await this.fileSystemService.SaveAll();
        }

        public async Task AssertDotnetBuildSucceeded()
        {
            await ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", "restore", TimeSpan.FromSeconds(15));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, "dotnet", "build", TimeSpan.FromSeconds(15));
        }

        public async Task AssertNpmInstallSucceeded()
        {
            var nodeDirectoryPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine).Split(';').First(x => x.Contains("nodejs"));
            var npmFilePath = Path.Combine(nodeDirectoryPath, "npm.cmd");
            await ProcessAssert.AssertStart(this.tempDirectoryPath, npmFilePath, "install", TimeSpan.FromMinutes(5));
        }

        public async Task AssertBowerInstallSucceeded()
        {
            await ProcessAssert.AssertStart(
                this.tempDirectoryPath,
                @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Web\External\bower.cmd",
                "install",
                TimeSpan.FromSeconds(30));
        }

        public async Task AssertGulpCleanBuildTestSucceeded()
        {
            var gulpFilePath = Path.Combine(this.tempDirectoryPath, @"node_modules\.bin\gulp.cmd");
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "clean", TimeSpan.FromSeconds(10));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "build", TimeSpan.FromSeconds(10));
            await ProcessAssert.AssertStart(this.tempDirectoryPath, gulpFilePath, "test", TimeSpan.FromSeconds(10));
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

        public void Dispose()
        {
            DirectoryExtended.Delete(this.tempDirectoryPath);
        }
    }
}
