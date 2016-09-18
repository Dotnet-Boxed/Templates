namespace Boilerplate.FeatureSelection.Test.Services
{
    using Boilerplate.FeatureSelection.Services;
    using Xunit;

    public class PortServiceTest
    {
        [Fact]
        public void GetRandomFreePort_NotSsl_1025To65535()
        {
            IPortService portService = new PortService();
            for (int i = 0; i < 10000; ++i)
            {
                int port = portService.GetRandomFreePort();
                Assert.InRange(port, 1025, 65535);
            }
        }

        [Fact]
        public void GetRandomFreePort_Ssl_44300To44399()
        {
            IPortService portService = new PortService();
            for (int i = 0; i < 200; ++i)
            {
                int port = portService.GetRandomFreePort(true);
                Assert.InRange(port, 44300, 44399);
            }
        }
    }
}
