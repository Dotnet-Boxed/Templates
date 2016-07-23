namespace Boilerplate.FeatureSelection.FunctionalTest
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Xunit;

    public class MVC6SampleTest
    {
        private readonly ProjectTemplateTester tester;

        public MVC6SampleTest()
        {
            this.tester = new ProjectTemplateTester(
                @"C:\GitHub\ASP.NET-MVC-Boilerplate\Source\MVC6\Boilerplate.AspNetCore.Sample\Boilerplate.AspNetCore.Sample.xproj");
        }

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
            this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .Last().IsSelected = true;

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
            this.tester.Features
                .OfType<MultiChoiceFeature>()
                .Where(x => x.IsVisible && !x.IsMultiSelect)
                .SelectMany(x => x.Items)
                .First().IsSelected = true;

            await this.tester.AddOrRemoveFeatures();

            await this.tester.AssertDotnetBuildSucceeded();
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

        [Theory]
        [InlineData(typeof(ApplicationInsightsFeature), true)]
        [InlineData(typeof(ApplicationInsightsFeature), false)]
        [InlineData(typeof(CstmlMinificationFeature), true)]
        [InlineData(typeof(CstmlMinificationFeature), false)]
        [InlineData(typeof(GooglePageSpeedFeature), true)]
        [InlineData(typeof(GooglePageSpeedFeature), false)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), true)]
        [InlineData(typeof(JavaScriptCodeStyleFeature), false)]
        [InlineData(typeof(JavaScriptHintFeature), true)]
        [InlineData(typeof(JavaScriptHintFeature), false)]
        [InlineData(typeof(TypeScriptFeature), true)]
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
            await this.tester.AssertBowerInstallSucceeded();
            await this.tester.AssertGulpCleanBuildTestSucceeded();
        }

        //[InlineData(typeof(JavaScriptTestFrameworkFeature), true)]
        //[InlineData(typeof(JavaScriptTestFrameworkFeature), false)]
    }
}
