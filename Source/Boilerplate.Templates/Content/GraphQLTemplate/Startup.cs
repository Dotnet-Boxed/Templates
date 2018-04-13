namespace ApiTemplate
{
    using System;
#if (CORS)
    using ApiTemplate.Constants;
#endif
    using ApiTemplate.Schemas;
    using Boilerplate.AspNetCore;
    using GraphQL;
    using GraphQL.Server.Transports.AspNetCore;
    using GraphQL.Server.Transports.WebSockets;
    using GraphQL.Server.Ui.Playground;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
#if (LoadBalancer)
    using Microsoft.AspNetCore.HttpOverrides;
#endif
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services) =>
            services
#if (ApplicationInsights)
                // Add Azure Application Insights data collection services to the services container.
                .AddApplicationInsightsTelemetry(this.configuration)
#endif
                .AddCustomCaching()
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
#if (ResponseCaching)
                .AddResponseCaching()
#endif
                .AddCustomResponseCompression()
                .AddSingleton<IDependencyResolver>(x => new FuncDependencyResolver(type => x.GetRequiredService(type)))
                .AddGraphQLHttp()
                // Add useful interface for accessing the ActionContext outside a controller.
                .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                // Add useful interface for accessing the HttpContext outside a controller.
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                // Add useful interface for accessing the IUrlHelper outside a controller.
                .AddScoped(x => x
                    .GetRequiredService<IUrlHelperFactory>()
                    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
                .AddMvcCore()
                    .AddApiExplorer()
                    .AddAuthorization()
                    .AddDataAnnotations()
                    .AddJsonFormatters()
                    .AddCustomJsonOptions()
#if (CORS)
                    .AddCustomCors()
#endif
                    .AddCustomMvcOptions(this.hostingEnvironment)
                .Services
                .AddProjectRepositories()
                .AddProjectGraphQLTypes()
                .AddProjectGraphQLQueries()
                .AddProjectGraphQLSchemas()
                .BuildServiceProvider();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        public void Configure(IApplicationBuilder application) =>
            application
#if (LoadBalancer)
                .UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedProto })
#endif
#if (ResponseCaching)
                .UseResponseCaching()
#endif
                .UseResponseCompression()
                .UseStaticFilesWithCacheControl()
                // Add the GraphQL playground UI to try out the GraphQL API. Not recommended to be run in production.
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x.UseGraphQLPlayground(new GraphQLPlaygroundOptions() { Path = "/" }))
#if (CORS)
                .UseCors(CorsPolicyName.AllowAny)
#endif
                .UseIf(
                    this.hostingEnvironment.IsDevelopment(),
                    x => x
                        .UseDebugging()
                        .UseDeveloperErrorPages())
#if (HttpsEverywhere)
                .UseIf(
                    !this.hostingEnvironment.IsDevelopment(),
                    x => x.UseStrictTransportSecurityHttpHeader())
#endif
                .UseWebSockets()
                .UseGraphQLWebSocket<CarsSchema>(new GraphQLWebSocketsOptions())
                .UseGraphQLHttp<CarsSchema>(new GraphQLHttpOptions());
    }
}