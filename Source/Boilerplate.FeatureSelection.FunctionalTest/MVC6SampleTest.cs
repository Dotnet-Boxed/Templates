namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;
    using Xunit;

    public class MVC6SampleTest : IDisposable
    {
        private readonly string projectDirectoryPath;
        private readonly string tempDirectoryPath;
        private readonly FeatureCollection features;
        private readonly IFileSystemService fileSystemService;

        public MVC6SampleTest()
        {
            this.projectDirectoryPath = ConfigurationManager.AppSettings["ProjectDirectoryPath"];
            this.tempDirectoryPath = ConfigurationManager.AppSettings["TempDirectoryPath"] + Guid.NewGuid().ToString();

            DirectoryExtended.Copy(this.projectDirectoryPath, this.tempDirectoryPath);

            var container = new ContainerBuilder()
                .RegisterServices(Path.Combine(this.tempDirectoryPath, "Boilerplate.AspNetCore.Sample.xproj"))
                .RegisterFeatureSet(FeatureSet.Mvc6)
                .Build();
            this.features = new FeatureCollection(container.Resolve<IEnumerable<IFeature>>());
            this.fileSystemService = container.Resolve<IFileSystemService>();
        }

        [Fact]
        public async Task MVC6Sample_Default_BuildsSuccessfully()
        {
            await this.AddOrRemoveFeatures();

            await this.AssertDotnetBuildSucceeded();
        }

        [Fact]
        public async Task MVC6Sample_Basic_BuildsSuccessfully()
        {
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .ToList()
                .ForEach(y => y.IsSelected = false);
            this.features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .Last().IsSelected = true;

            await this.AddOrRemoveFeatures();

            await this.AssertDotnetBuildSucceeded();
        }

        [Fact]
        public async Task MVC6Sample_Full_BuildsSuccessfully()
        {
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = true);
            this.features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .ToList()
                .ForEach(y => y.IsSelected = true);
            this.features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .First().IsSelected = true;

            await this.AddOrRemoveFeatures();

            await this.AssertDotnetBuildSucceeded();
        }

        [Theory]
        [InlineData(typeof(AboutPageFeature), true)]
        [InlineData(typeof(AboutPageFeature), false)]
        [InlineData(typeof(AndroidChromeM36ToM38FaviconFeature), true)]
        [InlineData(typeof(AndroidChromeM36ToM38FaviconFeature), false)]
        [InlineData(typeof(AndroidChromeM39FaviconsFeature), true)]
        [InlineData(typeof(AndroidChromeM39FaviconsFeature), false)]
        [InlineData(typeof(AppleIOSFaviconsFeature), true)]
        [InlineData(typeof(AppleIOSFaviconsFeature), false)]
        [InlineData(typeof(AppleMacSafariFaviconFeature), true)]
        [InlineData(typeof(AppleMacSafariFaviconFeature), false)]
        [InlineData(typeof(ApplicationInsightsFeature), true)]
        [InlineData(typeof(ApplicationInsightsFeature), false)]
        [InlineData(typeof(AuthorMetaTagFeature), true)]
        [InlineData(typeof(AuthorMetaTagFeature), false)]
        [InlineData(typeof(ContactPageFeature), true)]
        [InlineData(typeof(ContactPageFeature), false)]
        [InlineData(typeof(CstmlMinificationFeature), true)]
        [InlineData(typeof(CstmlMinificationFeature), false)]
        [InlineData(typeof(FeedFeature), true)]
        [InlineData(typeof(FeedFeature), false)]
        //[InlineData(typeof(GlimpseFeature), true)]
        //[InlineData(typeof(GlimpseFeature), false)]
        [InlineData(typeof(GoogleTvFaviconFeature), true)]
        [InlineData(typeof(GoogleTvFaviconFeature), false)]
        [InlineData(typeof(HttpExceptionFeature), true)]
        [InlineData(typeof(HttpExceptionFeature), false)]
        [InlineData(typeof(HttpsEverywhereFeature), true)]
        [InlineData(typeof(HttpsEverywhereFeature), false)]
        [InlineData(typeof(HumansTextFeature), true)]
        [InlineData(typeof(HumansTextFeature), false)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), true)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), false)]
        [InlineData(typeof(JavaScriptHintFeature), true)]
        [InlineData(typeof(JavaScriptHintFeature), false)]
        [InlineData(typeof(NWebSecFeature), true)]
        [InlineData(typeof(NWebSecFeature), false)]
        [InlineData(typeof(OpenGraphFeature), true)]
        [InlineData(typeof(OpenGraphFeature), false)]
        [InlineData(typeof(RedirectToCanonicalUrlFeature), true)]
        [InlineData(typeof(RedirectToCanonicalUrlFeature), false)]
        [InlineData(typeof(ReferrerMetaTagFeature), true)]
        [InlineData(typeof(ReferrerMetaTagFeature), false)]
        [InlineData(typeof(RobotsTextFeature), true)]
        [InlineData(typeof(RobotsTextFeature), false)]
        [InlineData(typeof(SearchFeature), true)]
        [InlineData(typeof(SearchFeature), false)]
        [InlineData(typeof(SitemapFeature), true)]
        [InlineData(typeof(SitemapFeature), false)]
        [InlineData(typeof(TwitterCardFeature), true)]
        [InlineData(typeof(TwitterCardFeature), false)]
        [InlineData(typeof(TypeScriptFeature), true)]
        [InlineData(typeof(TypeScriptFeature), false)]
        [InlineData(typeof(WebAppCapableFeature), true)]
        [InlineData(typeof(WebAppCapableFeature), false)]
        [InlineData(typeof(Windows81IE11EdgeFaviconFeature), true)]
        [InlineData(typeof(Windows81IE11EdgeFaviconFeature), false)]
        [InlineData(typeof(Windows8IE10FaviconFeature), true)]
        [InlineData(typeof(Windows8IE10FaviconFeature), false)]
        public async Task MVC6Sample_BinaryChoiceFeature_BuildsSuccessfully(Type type, bool isSelected)
        {
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .First(x => x.GetType() == type)
                .IsSelected = isSelected;

            await this.AddOrRemoveFeatures();

            await this.AssertDotnetBuildSucceeded();
        }

        [Theory]
        [InlineData(typeof(FrontEndFrameworkFeature), 0)]
        [InlineData(typeof(JavaScriptTestFrameworkFeature), 0)]
        [InlineData(typeof(JavaScriptTestFrameworkFeature), 2)]
        [InlineData(typeof(JsonSerializerSettingsFeature), 0)]
        [InlineData(typeof(JsonSerializerSettingsFeature), 1)]
        [InlineData(typeof(TargetFrameworkFeature), 0)]
        [InlineData(typeof(TargetFrameworkFeature), 1)]
        [InlineData(typeof(XmlFormatterFeature), 0)]
        [InlineData(typeof(XmlFormatterFeature), 1)]
        [InlineData(typeof(XmlFormatterFeature), 2)]
        [InlineData(typeof(XmlFormatterFeature), 3)]
        public async Task MVC6Sample_MultiChoiceFeature_BuildsSuccessfully(Type type, int selectedIndex)
        {
            this.features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.Items
                    .ToList()
                    .ForEach(y => y.IsSelected = x.Items.IndexOf(y) == selectedIndex));

            await this.AddOrRemoveFeatures();

            await this.AssertDotnetBuildSucceeded();
        }

        [Theory]
        [InlineData(typeof(ApplicationInsightsFeature), true)]
        [InlineData(typeof(ApplicationInsightsFeature), false)]
        [InlineData(typeof(CstmlMinificationFeature), true)]
        [InlineData(typeof(CstmlMinificationFeature), false)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), true)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), false)]
        [InlineData(typeof(JavaScriptHintFeature), true)]
        [InlineData(typeof(JavaScriptHintFeature), false)]
        [InlineData(typeof(TypeScriptFeature), true)]
        [InlineData(typeof(TypeScriptFeature), false)]
        public async Task MVC6Sample_BinaryChoiceFeature_GulpCleanBuildTestSuccessfull(Type type, bool isSelected)
        {
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .First(x => x.GetType() == type)
                .IsSelected = isSelected;

            await this.AddOrRemoveFeatures();

            await this.AssertNpmInstallSucceeded();
            await this.AssertBowerInstallSucceeded();
            await this.AssertGulpCleanBuildTestSucceeded();
        }


        //[InlineData(typeof(JavaScriptTestFrameworkFeature), true)]
        //[InlineData(typeof(JavaScriptTestFrameworkFeature), false)]

        public void Dispose()
        {
            DirectoryExtended.Delete(this.tempDirectoryPath);
        }

        private async Task AddOrRemoveFeatures()
        {
            foreach (IFeature feature in this.features)
            {
                await feature.AddOrRemoveFeature();
            }

            await this.fileSystemService.SaveAll();
        }

        private static async Task AssertStartProcess(
            string workingDirectory,
            string fileName,
            string arguments,
            TimeSpan timeout)
        {
            var showConsole = Debugger.IsAttached;

            using (var process = Process.Start(
                new ProcessStartInfo()
                {
                    CreateNoWindow = !showConsole,
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardError = !showConsole,
                    UseShellExecute = false,
                    WorkingDirectory = workingDirectory
                }))
            {
                var timedOut = !process.WaitForExit((int)timeout.TotalMilliseconds);

                var standardError = string.Empty;
                if (!showConsole)
                {
                    standardError = await process.StandardError.ReadToEndAsync();
                }

                var result = timedOut ? "Timed Out" : process.ExitCode == 0 ? "Succeeded" : "Failed";
                var message = $"Executing {fileName} {arguments} {result}.\r\n\r\nStandardError:\r\n{standardError}";
                if (timedOut || process.ExitCode != 0)
                {
                    Assert.False(true, message);
                }
                else
                {
                    Debug.WriteLine(message);
                }
            }
        }

        private async Task AssertDotnetBuildSucceeded()
        {
            await AssertStartProcess(this.tempDirectoryPath, "dotnet", "restore", TimeSpan.FromSeconds(15));
            await AssertStartProcess(this.tempDirectoryPath, "dotnet", "build", TimeSpan.FromSeconds(15));
        }

        private async Task AssertNpmInstallSucceeded()
        {
            var nodeDirectoryPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine).Split(';').First(x => x.Contains("nodejs"));
            var npmFilePath = Path.Combine(nodeDirectoryPath, "npm.cmd");
            await AssertStartProcess(this.tempDirectoryPath, npmFilePath, "install", TimeSpan.FromMinutes(5));
        }

        private async Task AssertBowerInstallSucceeded()
        {
            await AssertStartProcess(
                this.tempDirectoryPath,
                @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Web\External\bower.cmd",
                "install",
                TimeSpan.FromSeconds(30));
        }

        private async Task AssertGulpCleanBuildTestSucceeded()
        {
            var gulpFilePath = Path.Combine(this.tempDirectoryPath, @"node_modules\.bin\gulp.cmd");
            await AssertStartProcess(this.tempDirectoryPath, gulpFilePath, "clean", TimeSpan.FromSeconds(10));
            await AssertStartProcess(this.tempDirectoryPath, gulpFilePath, "build", TimeSpan.FromSeconds(10));
            await AssertStartProcess(this.tempDirectoryPath, gulpFilePath, "test", TimeSpan.FromSeconds(10));
        }
    }
}
