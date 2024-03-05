using Network.Server.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using HttpRequestHeaders = Network.Server.Headers.HttpRequestHeaders;

namespace Network.Server.Handlers
{
    public class RequestListener
    {
        private Socket _handler;
        private string? _tail; 

        private HttpHeaders? _headers;
        private string? _body;

        public RequestListener(Socket handler)
        {
            _handler = handler;

            _tail = null;
            _headers = null;
            _body = null;
        }

        public async Task<HttpHeaders> ReadHeadersAsync()
        {
            if (_headers != null)
                return _headers;

            HttpRequestHeaders headers = new();

            int received = 0;
            byte[] buffer = new byte[256];
            do
            { 
                received = await _handler.ReceiveAsync(buffer);
                string requestMessage = Encoding.UTF8.GetString(buffer, 0, received);
                Console.WriteLine(requestMessage);
                if (requestMessage.Contains("\r\n\r\n"))
                    break;
            }
            while (true);

            return headers;
        }

        public async Task<string> ReadBodyAsync()
        {
            if (_body != null)
                return _body;

            return "";
        }
    }
}
