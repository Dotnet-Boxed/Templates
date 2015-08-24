namespace Boilerplate.Wizard
{
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
            builder.RegisterType<HumansTextFeature>().As<IFeature>().SingleInstance();
            builder.RegisterType<FrontEndFrameworkFeature>().As<IFeature>().SingleInstance();

            // Services
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
    }
}
