namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Xunit;

    public class MVC6SampleTest : IDisposable
    {
        private readonly ProjectTemplateTester tester;

        public MVC6SampleTest() =>
            this.tester = new ProjectTemplateTester(
                ConfigurationService.ProjectFilePath,
                ConfigurationService.TempDirectoryPath);

        [Fact]
        public async Task MVC6Sample_Default_BuildsSuccessfully()
        {
            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
            // await this.tester.AssertSiteStartsAndResponds("/");
        }

        [Fact]
        public async Task MVC6Sample_Basic_BuildsSuccessfully()
        {
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .ToList()
                .ForEach(y => y.IsSelected = false);
            foreach (var feature in this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect))
            {
                feature.Items.ToList().ForEach(x => x.IsSelected = false);
                feature.Items.Last().IsSelected = true;
            }

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
        }

        [Fact]
        public async Task MVC6Sample_Full_BuildsSuccessfully()
        {
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = true);
            this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .ToList()
                .ForEach(y => y.IsSelected = true);
            foreach (var feature in this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect))
            {
                feature.Items.ToList().ForEach(x => x.IsSelected = false);
                feature.Items.First().IsSelected = true;
            }

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
        }

        [Theory]
        [InlineData(typeof(AboutPageFeature), false)]
        [InlineData(typeof(AndroidChromeM36ToM38FaviconFeature), false)]
        [InlineData(typeof(AndroidChromeM39FaviconsFeature), false)]
        [InlineData(typeof(AppleIOSFaviconsFeature), false)]
        [InlineData(typeof(AppleMacSafariFaviconFeature), false)]
        [InlineData(typeof(ApplicationInsightsFeature), true)]
        [InlineData(typeof(AuthorMetaTagFeature), false)]
        [InlineData(typeof(CloudProviderFeature), false)]
        [InlineData(typeof(ContactPageFeature), false)]
        [InlineData(typeof(CorsFeature), true)]
        [InlineData(typeof(FeedFeature), false)]
        //[InlineData(typeof(GlimpseFeature), true)]
        //[InlineData(typeof(GlimpseFeature), false)]
        [InlineData(typeof(GoogleTvFaviconFeature), true)]
        [InlineData(typeof(HttpExceptionFeature), true)]
        [InlineData(typeof(HttpsEverywhereFeature), false)]
        [InlineData(typeof(HumansTextFeature), false)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), false)]
        [InlineData(typeof(JavaScriptHintFeature), false)]
        [InlineData(typeof(NWebSecFeature), false)]
        [InlineData(typeof(OpenGraphFeature), false)]
        [InlineData(typeof(RedirectToCanonicalUrlFeature), false)]
        [InlineData(typeof(ReferrerMetaTagFeature), false)]
        [InlineData(typeof(RobotsTextFeature), false)]
        [InlineData(typeof(SearchFeature), false)]
        [InlineData(typeof(SitemapFeature), false)]
        [InlineData(typeof(TwitterCardFeature), false)]
        [InlineData(typeof(TypeScriptFeature), false)]
        [InlineData(typeof(WebAppCapableFeature), true)]
        [InlineData(typeof(Windows81IE11EdgeFaviconFeature), false)]
        [InlineData(typeof(Windows8IE10FaviconFeature), true)]
        public async Task MVC6Sample_BinaryChoiceFeature_BuildsSuccessfully(Type type, bool isSelected)
        {
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .First(x => x.GetType() == type)
                .IsSelected = isSelected;

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
        }

        [Theory]
        [InlineData(typeof(PrimaryWebServerFeature), 1)]
        [InlineData(typeof(ReverseProxyWebServerFeature), 1)]
        [InlineData(typeof(JavaScriptTestFrameworkFeature), 1)]
        [InlineData(typeof(JsonSerializerSettingsFeature), 1)]
        [InlineData(typeof(TargetFrameworkFeature), 0)]
        [InlineData(typeof(TargetFrameworkFeature), 1)]
        [InlineData(typeof(XmlFormatterFeature), 1)]
        [InlineData(typeof(XmlFormatterFeature), 2)]
        [InlineData(typeof(XmlFormatterFeature), 3)]
        public async Task MVC6Sample_MultiChoiceFeature_BuildsSuccessfully(Type type, int selectedIndex)
        {
            this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.Items
                    .ToList()
                    .ForEach(y => y.IsSelected = x.Items.IndexOf(y) == selectedIndex));

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
        }

        [Theory(Skip = "Skip NPM tests")]
        [InlineData(typeof(ApplicationInsightsFeature), true)]
        [InlineData(typeof(GooglePageSpeedFeature), true)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), false)]
        [InlineData(typeof(JavaScriptHintFeature), false)]
        [InlineData(typeof(TypeScriptFeature), false)]
        public async Task MVC6Sample_BinaryChoiceFeature_GulpCleanBuildTestSuccessfull(Type type, bool isSelected)
        {
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .ToList()
                .ForEach(x => x.IsSelected = false);
            this.tester.Features
                .OfType<BinaryChoiceFeature>()
                .Where(x => x.IsVisible)
                .First(x => x.GetType() == type)
                .IsSelected = isSelected;

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertNpmInstallSucceeded();
            await this.tester.AssertGulpCleanBuildTestSucceeded();
        }

        public void Dispose()
        {
            this.tester.Dispose();
        }
    }
}
