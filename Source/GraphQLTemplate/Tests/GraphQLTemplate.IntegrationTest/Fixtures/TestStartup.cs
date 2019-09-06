namespace GraphQLTemplate.IntegrationTest.Fixtures
{
    // using GraphQLTemplate.Services;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;

    public class TestStartup : Startup
    {
        // private readonly Mock<IClockService> clockServiceMock;

        public TestStartup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
            : base(configuration, hostingEnvironment)
        {
            // this.clockServiceMock = new Mock<IClockService>(MockBehavior.Strict);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            // services
            //     .AddSingleton(this.clockServiceMock);

            base.ConfigureServices(services);

            // services
            //     .AddSingleton(this.clockServiceMock.Object);
        }
    }
}
