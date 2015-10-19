namespace Boilerplate.Wizard
{
    using Autofac;
    using Boilerplate.Wizard.Features;
    using Boilerplate.Wizard.Services;
    using Boilerplate.Wizard.ViewModels;
    using Boilerplate.Wizard.Views;

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
                    builder.RegisterType<SetRandomPortsFeature>().As<IFeature>().SingleInstance();
                    // CSS and JavaScript
                    builder.RegisterType<FrontEndFrameworkFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<TypeScriptFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<JavaScriptCodeStyleFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<JavaScriptTestFrameworkFeature>().As<IFeature>().SingleInstance();
                    // Formatters
                    builder.RegisterType<JsonSerializerSettingsFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<BsonFormatterFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<XmlFormatterFeature>().As<IFeature>().SingleInstance();
                    // Performance
                    builder.RegisterType<CstmlMinificationFeature>().As<IFeature>().SingleInstance();
                    // Security
                    builder.RegisterType<HttpsEverywhereFeature>().As<IFeature>().SingleInstance();
                    // SEO
                    builder.RegisterType<RobotsTextFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<SitemapFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<RedirectToCanonicalUrlFeature>().As<IFeature>().SingleInstance();
                    // Pages
                    builder.RegisterType<AboutPageFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<ContactPageFeature>().As<IFeature>().SingleInstance();
                    // Social
                    builder.RegisterType<AuthorMetaTagFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<OpenGraphFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<TwitterCardFeature>().As<IFeature>().SingleInstance();
                    // Favicons
                    builder.RegisterType<AppleIOSFaviconsFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<AppleMacSafariFaviconFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<AndroidChromeM36ToM38FaviconFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<AndroidChromeM39FaviconsFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<GoogleTvFaviconFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<Windows8IE10FaviconFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<Windows81IE11EdgeFaviconFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<WebAppCapableFeature>().As<IFeature>().SingleInstance();
                    // Other
                    builder.RegisterType<FeedFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<SearchFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<HumansTextFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<ReferrerMetaTagFeature>().As<IFeature>().SingleInstance();
                    break;
                case FeatureSet.Mvc6Api:
                    // Hidden
                    builder.RegisterType<SetRandomPortsFeature>().As<IFeature>().SingleInstance();
                    // Rest
                    builder.RegisterType<SwaggerFeature>().As<IFeature>().SingleInstance();
                    // Formatters
                    builder.RegisterType<JsonSerializerSettingsFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<BsonFormatterFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<XmlFormatterFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<NoContentFormatterFeature>().As<IFeature>().SingleInstance();
                    builder.RegisterType<NotAcceptableFormatterFeature>().As<IFeature>().SingleInstance();
                    // Security
                    builder.RegisterType<HttpsEverywhereFeature>().As<IFeature>().SingleInstance();
                    break;
            }
        }
    }
}
