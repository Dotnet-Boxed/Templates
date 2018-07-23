namespace Boxed.Templates.FunctionalTest
{
    using System.Net;
    using System.Net.Sockets;

    public static class PortHelper
    {
        public static int GetFreeTcpPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }
}
