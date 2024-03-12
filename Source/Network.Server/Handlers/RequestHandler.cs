using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Network.Server.Execution;
using Network.Server.Request;

namespace Network.Server.Handlers
{
    public class RequestHandler
        (RequestExecutor executor) 
        : IRequestHandler
    {
        private readonly RequestExecutor _executor = executor;
        public async Task ProcessingAsync(Socket tcpHandler)
        {
            RequestEnumerator enumerator = new(tcpHandler);
            HttpRequest request = await HttpRequest.ParseAsync(enumerator));
            
            _executor.ExecuteAsync(new HttpContext(request, tcpHandler));
        
            await tcpHandler.DisconnectAsync(false);
        }
    }
}
