namespace Boilerplate.FeatureSelection
{
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;

    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder RegisterFeatureSet(this ContainerBuilder builder, FeatureSet featureSet)
        {
            builder.Register(x => featureSet);

            switch (featureSet)
            {
                case FeatureSet.Mvc6:
                    // Hidden
                    builder.RegisterType<NoJavaScriptLintingFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<RemoveServicesNamespaceFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<SetRandomPortsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<AssemblyCopyrightFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<NpmPackageNameFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Target Framework
                    builder.RegisterType<TargetFrameworkFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Hosting
                    builder.RegisterType<PrimaryWebServerFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<ReverseProxyWebServerFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<CloudProviderFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // CSS and JavaScript
                    builder.RegisterType<FrontEndFrameworkFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<TypeScriptFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<JavaScriptCodeStyleFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<JavaScriptHintFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<JavaScriptTestFrameworkFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Formatters
                    builder.RegisterType<JsonSerializerSettingsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<XmlFormatterFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Performance
                    builder.RegisterType<GooglePageSpeedFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Security
                    builder.RegisterType<HttpsEverywhereFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<CorsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<NWebSecFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Monitoring
                    builder.RegisterType<ApplicationInsightsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Debugging
                    builder.RegisterType<GlimpseFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // SEO
                    builder.RegisterType<RobotsTextFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<SitemapFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<RedirectToCanonicalUrlFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Pages
                    builder.RegisterType<AboutPageFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<ContactPageFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Social
                    builder.RegisterType<AuthorMetaTagFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<OpenGraphFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<TwitterCardFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Favicons
                    builder.RegisterType<AppleIOSFaviconsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<AppleMacSafariFaviconFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<AndroidChromeM36ToM38FaviconFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<AndroidChromeM39FaviconsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<GoogleTvFaviconFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<Windows8IE10FaviconFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<Windows81IE11EdgeFaviconFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<WebAppCapableFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Other
                    builder.RegisterType<FeedFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<SearchFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<HumansTextFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<ReferrerMetaTagFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<HttpExceptionFeature>().AsSelf().As<IFeature>().SingleInstance();
                    break;
            }

            return builder;
        }

        public static ContainerBuilder RegisterServices(this ContainerBuilder builder, string projectFilePath)
        {
            builder.RegisterType<FileSystemService>().As<IFileSystemService>().SingleInstance();
            builder.RegisterType<PortService>().As<IPortService>().SingleInstance();
            builder.RegisterType<ProjectService>()
                .As<IProjectService>()
                .WithParameter("projectFilePath", projectFilePath)
                .SingleInstance();
            builder.RegisterType<JsonFileFixerService>().As<IFileFixerService>().SingleInstance();
            return builder;
        }
    }
}
