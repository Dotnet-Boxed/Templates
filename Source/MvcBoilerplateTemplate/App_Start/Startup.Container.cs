namespace $safeprojectname$
{
    using System.Reflection;
    using System.Web.Mvc;
    using Autofac;
    using Autofac.Integration.Mvc;
    using $safeprojectname$.Services;
    using Owin;

    /// <summary>
    /// Register types into the Autofac Inversion of Control (IOC) container.
    /// </summary>
    public partial class Startup
    {
        public static void ConfigureContainer(IAppBuilder app)
        {
            IContainer container = CreateContainer();
            app.UseAutofacMiddleware(container);

            // Register MVC Types 
            app.UseAutofacMvc();
        }

        private static IContainer CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetExecutingAssembly();

            RegisterServices(builder);
            RegisterMvc(builder, assembly);

            IContainer container = builder.Build();

            SetMvcDependencyResolver(container);

            return container;
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<FeedService>().As<IFeedService>().InstancePerRequest();
            builder.RegisterType<LoggingService>().As<ILoggingService>().SingleInstance();
            builder.RegisterType<OpenSearchService>().As<IOpenSearchService>().InstancePerRequest();
            builder.RegisterType<RobotsService>().As<IRobotsService>().InstancePerRequest();
            builder.RegisterType<SitemapService>().As<ISitemapService>().InstancePerRequest();
        }

        private static void RegisterMvc(ContainerBuilder builder, Assembly assembly)
        {
            // Register Common MVC Types
            builder.RegisterModule<AutofacWebTypesModule>();

            // Register MVC Filters
            builder.RegisterFilterProvider();

            // Register MVC Controllers
            builder.RegisterControllers(assembly);
        }


        /// <summary>
        /// Sets the ASP.NET MVC dependency resolver.
        /// </summary>
        /// <param name="container">The container.</param>
        private static void SetMvcDependencyResolver(IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}