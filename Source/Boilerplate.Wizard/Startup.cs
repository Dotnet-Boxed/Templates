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
            // Hidden
            builder.RegisterType<SetRandomPortsFeature>().As<IFeature>().SingleInstance();

            if (featureSet == FeatureSet.Mvc6)
            {
                // CSS and JavaScript
                builder.RegisterType<FrontEndFrameworkFeature>().As<IFeature>().SingleInstance();
                builder.RegisterType<TypeScriptFeature>().As<IFeature>().SingleInstance();
                builder.RegisterType<JavaScriptTestFrameworkFeature>().As<IFeature>().SingleInstance();
            }

            if (featureSet == FeatureSet.Mvc6Api)
            {
                // Rest
                builder.RegisterType<SwaggerFeature>().As<IFeature>().SingleInstance();
            }

            // Formatters
            builder.RegisterType<JsonSerializerSettingsFeature>().As<IFeature>().SingleInstance();
            builder.RegisterType<BsonFormatterFeature>().As<IFeature>().SingleInstance();
            builder.RegisterType<XmlFormatterFeature>().As<IFeature>().SingleInstance();
            if (featureSet == FeatureSet.Mvc6Api)
            {
                builder.RegisterType<NoContentFormatterFeature>().As<IFeature>().SingleInstance();
                builder.RegisterType<NotAcceptableFormatterFeature>().As<IFeature>().SingleInstance();
            }

            // Security
            builder.RegisterType<HttpsEverywhereFeature>().As<IFeature>().SingleInstance();

            if (featureSet == FeatureSet.Mvc6)
            {
                // Other
                builder.RegisterType<HumansTextFeature>().As<IFeature>().SingleInstance();
            }
        }
    }
}
