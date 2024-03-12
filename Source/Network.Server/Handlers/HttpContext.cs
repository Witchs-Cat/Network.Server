using Network.Server.Request;
using System.Net.Sockets;

namespace Network.Server.Handlers
{
    public class HttpContext
        (HttpRequest request, Socket socket)
    {
        public HttpRequest Request { get; } = request;
        public Socket Socket { get; } = socket;
    }
}