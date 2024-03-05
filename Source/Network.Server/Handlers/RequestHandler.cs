using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        public RequestHandler() 
        {
        }

        public async Task ProcessingAsync(Socket tcpHandler)
        {
            RequestListener requestListener = new(tcpHandler);
            await requestListener.ReadHeadersAsync();

            byte[] echoBytes = Encoding.UTF8.GetBytes("<h1>Hello</h1>");
            await tcpHandler.SendAsync(echoBytes);
            await tcpHandler.DisconnectAsync(true);
        }
    }
}
