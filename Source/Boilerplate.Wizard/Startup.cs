namespace Boilerplate.Wizard
{
    using System;
    using System.Threading;
    using Autofac;
    using Boilerplate.Wizard.Features;
    using Boilerplate.Wizard.Services;
    using Boilerplate.Wizard.ViewModels;
    using Boilerplate.Wizard.Views;

    public class Startup
    {
        //static void Main()
        //{
        //    Thread thread = new Thread(x =>
        //    {
        //        new Startup().Execute(@"C:\Temp\New folder\1");
        //    });
        //    thread.SetApartmentState(ApartmentState.STA);
        //    thread.Start();
        //}

        public void Execute(string projectFilePath)
        {
            IContainer container = CreateContainer(projectFilePath);
            IMainView mainView = container.Resolve<IMainView>();
            mainView.ShowDialog();
        }

        private static IContainer CreateContainer(string projectFilePath)
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Features
            RegisterFeatures(builder);

            // Services
            builder.RegisterType<FileSystemService>().As<IFileSystemService>().SingleInstance();
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

        private static void RegisterFeatures(ContainerBuilder builder)
        {
            // CSS and JavaScript
            builder.RegisterType<FrontEndFrameworkFeature>().As<IFeature>().SingleInstance();

            // Formatters
            builder.RegisterType<JsonFormatterFeature>().As<IFeature>().SingleInstance();
            builder.RegisterType<XmlFormatterFeature>().As<IFeature>().SingleInstance();
            // builder.RegisterType<BsonFormatterFeature>().As<IFeature>().SingleInstance();

            // Other
            builder.RegisterType<HumansTextFeature>().As<IFeature>().SingleInstance();
        }
    }
}
