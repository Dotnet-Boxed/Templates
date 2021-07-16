namespace GraphQLTemplate
{
    using Boxed.AspNetCore;
#if CORS
    using GraphQLTemplate.Constants;
    using HotChocolate.AspNetCore;
#endif
    using Microsoft.AspNetCore.Builder;
#if HealthCheck
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored (See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html).</param>
        /// <param name="webHostEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default (See http://docs.asp.net/en/latest/fundamentals/environments.html).</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime (See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx).
        /// </summary>
        /// <param name="services">The services.</param>
        public virtual void ConfigureServices(IServiceCollection services) =>
            services
#if ApplicationInsights
                // Add Azure Application Insights data collection services to the services container.
                .AddApplicationInsightsTelemetry(this.configuration)
#endif
                .AddCustomCaching()
#if CORS
                .AddCustomCors()
#endif
                .AddCustomOptions(this.configuration)
                .AddCustomRouting()
#if ResponseCompression
                .AddCustomResponseCompression(this.configuration)
#endif
#if HttpsEverywhere
                .AddCustomStrictTransportSecurity()
#endif
#if HealthCheck
                .AddCustomHealthChecks(this.webHostEnvironment, this.configuration)
#endif
#if OpenTelemetry
                .AddCustomOpenTelemetryTracing(this.webHostEnvironment)
#endif
                .AddHttpContextAccessor()
                .AddServerTiming()
                .AddControllers()
                    .AddCustomJsonOptions(this.webHostEnvironment)
                    .AddCustomMvcOptions(this.configuration)
                .Services
#if Authorization
                .AddCustomAuthorization()
#endif
#if Redis
                .AddCustomRedis(this.webHostEnvironment, this.configuration)
#endif
                .AddCustomGraphQL(this.webHostEnvironment, this.configuration)
                .AddProjectMappers()
                .AddProjectServices()
                .AddProjectRepositories();

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        /// <param name="application">The application builder.</param>
        public virtual void Configure(IApplicationBuilder application) =>
            application
                .UseIf(
                    this.webHostEnvironment.IsDevelopment(),
                    x => x.UseServerTiming())
#if ForwardedHeaders
                .UseForwardedHeaders()
#elif HostFiltering
                .UseHostFiltering()
#endif
                .UseRouting()
#if CORS
                .UseCors(CorsPolicyName.AllowAny)
#endif
#if ResponseCompression
                .UseResponseCompression()
#endif
#if HttpsEverywhere
                .UseIf(
                    !this.webHostEnvironment.IsDevelopment(),
                    x => x.UseHsts())
#endif
                .UseIf(
                    this.webHostEnvironment.IsDevelopment(),
                    x => x.UseDeveloperExceptionPage())
#if Subscriptions
                .UseWebSockets()
#endif
                .UseStaticFilesWithCacheControl()
#if Serilog
                .UseCustomSerilogRequestLogging()
#endif
                .UseEndpoints(
                    builder =>
                    {
                        var graphQLServerOptions = new GraphQLServerOptions();
                        // Add Banana Cake Pop GraphQL client at /graphql.
                        graphQLServerOptions.Tool.Enable = this.webHostEnvironment.IsDevelopment();
                        builder.MapGraphQL().WithOptions(graphQLServerOptions);
#if HealthCheck
#if CORS
                        builder
                            .MapHealthChecks("/status")
                            .RequireCors(CorsPolicyName.AllowAny);
                        builder
                            .MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false })
                            .RequireCors(CorsPolicyName.AllowAny);
#else
                        builder.MapHealthChecks("/status");
                        builder.MapHealthChecks("/status/self", new HealthCheckOptions() { Predicate = _ => false });
#endif
#endif
                    })
                .UseIf(
                    this.webHostEnvironment.IsDevelopment(),
                    x => x
                        // Add the GraphQL Playground UI to try out the GraphQL API at /.
                        .UseGraphQLPlayground("/")
                        // Add the GraphQL Voyager UI to let you navigate your GraphQL API as a spider graph at /voyager.
                        .UseGraphQLVoyager("/voyager"));
    }
}
