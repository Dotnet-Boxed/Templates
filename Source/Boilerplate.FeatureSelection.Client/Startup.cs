namespace Boilerplate.FeatureSelection
{
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
    using Boilerplate.FeatureSelection.Services;
    using Boilerplate.FeatureSelection.ViewModels;
    using Boilerplate.FeatureSelection.Views;

    public class Startup
    {
        //static void Main()
        //{
        //    var thread = new System.Threading.Thread(x =>
        //    {
        //        new Startup().Execute(@"C:\Temp\New folder\1", FeatureSet.Mvc6);
        //    });
        //    thread.SetApartmentState(System.Threading.ApartmentState.STA);
        //    thread.Start();
        //}

        public void Execute(string projectFilePath, FeatureSet featureSet)
        {
            IContainer container = CreateContainer(projectFilePath, featureSet);
            IMainView mainView = container.Resolve<IMainView>();
            mainView.ShowDialog();
        }

        private static IContainer CreateContainer(string projectFilePath, FeatureSet featureSet)
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Features
            builder.Register(x => featureSet);
            RegisterFeatures(builder, featureSet);

            // Services
            builder.RegisterType<FileSystemService>().As<IFileSystemService>().SingleInstance();
            builder.RegisterType<PortService>().As<IPortService>().SingleInstance();
            builder.RegisterType<ProjectService>()
                .As<IProjectService>()
                .WithParameter("projectFilePath", projectFilePath)
                .SingleInstance();
            builder.RegisterType<JsonFileFixerService>().As<IFileFixerService>().SingleInstance();

            // View Models
            builder.RegisterType<MainViewModel>().As<IMainViewModel>().SingleInstance();

            // Views
            builder.RegisterType<MainView>()
                .As<IMainView>()
                .PropertiesAutowired(PropertyWiringOptions.None)
                .SingleInstance();

            return builder.Build();
        }

        private static void RegisterFeatures(ContainerBuilder builder, FeatureSet featureSet)
        {
            switch (featureSet)
            {
                case FeatureSet.Mvc6:
                    // Hidden
                    builder.RegisterType<JavaScriptLintFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<ServicesFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<SetRandomPortsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<AssemblyCopyrightFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<NpmPackageNameFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Target Framework
                    builder.RegisterType<TargetFrameworkFeature>().AsSelf().As<IFeature>().SingleInstance();
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
                    builder.RegisterType<CstmlMinificationFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Security
                    builder.RegisterType<HttpsEverywhereFeature>().AsSelf().As<IFeature>().SingleInstance();
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
                case FeatureSet.Mvc6Api:
                    // Hidden
                    builder.RegisterType<SetRandomPortsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Rest
                    builder.RegisterType<SwaggerFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Formatters
                    builder.RegisterType<JsonSerializerSettingsFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<XmlFormatterFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<NoContentFormatterFeature>().AsSelf().As<IFeature>().SingleInstance();
                    builder.RegisterType<NotAcceptableFormatterFeature>().AsSelf().As<IFeature>().SingleInstance();
                    // Security
                    builder.RegisterType<HttpsEverywhereFeature>().AsSelf().As<IFeature>().SingleInstance();
                    break;
            }
        }
    }
}
