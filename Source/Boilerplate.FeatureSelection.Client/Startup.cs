namespace Boilerplate.FeatureSelection
{
    using Autofac;
    using Boilerplate.FeatureSelection.Features;
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
            builder.RegisterFeatureSet(featureSet);

            // Services
            builder.RegisterServices(projectFilePath);

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
