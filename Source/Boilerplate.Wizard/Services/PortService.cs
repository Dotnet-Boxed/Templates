namespace Boilerplate.Wizard.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    public class PortService : IPortService
    {
        private static Random random = new Random();

        public int GetRandomFreePort(bool ssl = false)
        {
            int port;
            if (ssl)
            {
                // https://www.iis.net/learn/extensions/using-iis-express/handling-url-binding-failures-in-iis-express
                port = GetRandomFreePort(44300, 44399, 50, "https://*:{0}/");
            }
            else
            {
                // IANA suggests the range 49152 to 65535 for dynamic or private ports.
                port = GetRandomFreePort(1025, 65535, 200, "http://*:{0}/");
            }

            return port;
        }

        private int GetRandomFreePort(int portStart, int portEnd, int retryLimit, string url)
        {
            int port = -1;

            List<int> usedPorts = new List<int>();
            while (true)
            {
                if (usedPorts.Count > retryLimit)
                {
                    port = portStart;
                    break;
                }

                port = random.Next(portStart, portEnd);
                if (usedPorts.Contains(port))
                {
                    continue;
                }
                else
                {
                    usedPorts.Add(port);
                }

                using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    try
                    {
                        var ipAddress = Dns.GetHostEntry("localhost").AddressList
                            .Where(x => x.AddressFamily == AddressFamily.InterNetwork)
                            .First();
                        socket.Bind(new IPEndPoint(ipAddress, port));
                    }
                    catch
                    {
                        continue;
                    }
                }

                break;
            }

            return port;
        }
    }
}
