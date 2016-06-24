namespace Boilerplate.FeatureSelection.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Gets random free ports on the current machine.
    /// </summary>
    public class PortService : IPortService
    {
        #region Fields

        private const int HttpsPortStart = 44300;
        private const int HttpsPortEnd = 44399;
        private const int HttpsRetryLimit = 50;
        private const string HttpsUrl = "https://*:{0}/";

        private const int HttpPortStart = 1025;
        private const int HttpPortEnd = 65535;
        private const int HttpRetryLimit = 300;
        private const string HttpUrl = "http://*:{0}/";

        private const string Localhost = "localhost";

        private static Random random = new Random();

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Gets a random free port on the machine by randomly test ports until a free one is found.
        /// </summary>
        /// <param name="https">Find a free HTTPS port between 44300 and 44399, otherwise find a port between 1025 and 65535.</param>
        /// <returns>A port number.</returns>
        public int GetRandomFreePort(bool https = false)
        {
            int port;
            if (https)
            {
                // https://www.iis.net/learn/extensions/using-iis-express/handling-url-binding-failures-in-iis-express
                port = GetRandomFreePort(HttpsPortStart, HttpsPortEnd, HttpsRetryLimit, HttpsUrl);
            }
            else
            {
                // IANA suggests the range 49152 to 65535 for dynamic or private ports.
                port = GetRandomFreePort(HttpPortStart, HttpPortEnd, HttpRetryLimit, HttpUrl);
            }

            return port;
        }

        #endregion

        #region Private Static Methods

        private static int GetRandomFreePort(int portStart, int portEnd, int retryLimit, string url)
        {
            int port = -1;

            List<int> usedPorts = new List<int>();
            while (true)
            {
                if (usedPorts.Count > retryLimit)
                {
                    // Get a random port and hope it's free.
                    port = random.Next(portStart, portEnd);
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
                        var ipAddress = Dns.GetHostEntry(Localhost).AddressList
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

        #endregion
    }
}
