namespace GraphQLTemplate;

using Boxed.AspNetCore;
#if CORS
using GraphQLTemplate.Constants;
#endif
using HotChocolate.AspNetCore;
#if HealthCheck
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
#endif
using Microsoft.Extensions.DependencyInjection;
#if Serilog
using Serilog;
#endif

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
            .AddMemoryCache()
#if DistributedCacheInMemory
            .AddDistributedMemoryCache()
#elif DistributedCacheRedis
            .AddStackExchangeRedisCache(options => { })
#endif
#if CORS
            .AddCors()
#endif
#if ResponseCompression
            .AddResponseCompression()
#endif
            .AddRouting()
#if HttpsEverywhere
            .AddHsts(options => { })
#endif
#if HealthCheck
            .AddCustomHealthChecks(this.webHostEnvironment, this.configuration)
#endif
#if OpenTelemetry
            .AddOpenTelemetryTracing(builder => builder.AddCustomTracing(this.webHostEnvironment))
#endif
            .AddHttpContextAccessor()
            .AddServerTiming()
#if Authorization
            .AddAuthorization()
#endif
#if Redis
            .AddCustomRedis(this.webHostEnvironment, this.configuration)
#endif
            .AddCustomGraphQL(this.webHostEnvironment, this.configuration)
            .AddCustomOptions(this.configuration)
            .AddCustomConfigureOptions()
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
            .UseStaticFiles()
#if Serilog
            .UseSerilogRequestLogging()
#endif
            .UseEndpoints(
                builder =>
                {
                    var options = new GraphQLServerOptions();
                    options.Tool.Enable = false;
                    // Map the GraphQL HTTP and web socket endpoint at /graphql.
                    builder.MapGraphQL().WithOptions(options);

                    if (this.webHostEnvironment.IsDevelopment())
                    {
                        // Map the GraphQL Playground UI to try out the GraphQL API at /.
                        builder.MapGraphQLPlayground("/");
                        // Map the GraphQL Voyager UI to let you navigate your GraphQL API as a spider graph at /voyager.
                        builder.MapGraphQLVoyager("/voyager");
                        // Map the GraphQL Banana Cake Pop UI to let you navigate your GraphQL API at /banana.
                        builder.MapBananaCakePop("/banana");
                    }

                    // Map health check endpoints.
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
                });
}
