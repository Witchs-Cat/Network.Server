using System.Net.Sockets;

namespace Network.Server.Handlers
{
    public interface IRequestHandler
    {
        Task ProcessingAsync(Socket tcpClient);
    }
}