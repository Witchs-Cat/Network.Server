using System.Net;

namespace Network.Server
{
    public class ServerConfig
    {
        public EndPoint EndPoint { get; internal set; } = new IPEndPoint(IPAddress.Any, 8080);
        public int Backlog { get; internal set; } = 100;
    }
}