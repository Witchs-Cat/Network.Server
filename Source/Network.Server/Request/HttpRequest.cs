using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Request
{
    public class HttpRequest(
        RequestInfo info,
        HttpRequestHeaders headers,
        RequestEnumerator enumerator)
    {
        private RequestEnumerator _enumerator = enumerator;

        public RequestInfo Info { get; } = info;
        public HttpRequestHeaders Headers { get; } = headers;

        public Task<byte[]> ReadBodyAsync()
        {
            throw new NotImplementedException();
        }

        public static async Task<HttpRequest> ParseAsync(RequestEnumerator enumerator)
        {
            string headerLine = await enumerator.ReadLineAsync();
            RequestInfo info = RequestInfo.ParseFromString(headerLine.Replace("\r\n", ""));
            HttpRequestHeaders headers = new();
            do
            {
                headerLine = await enumerator.ReadLineAsync();
                headerLine = headerLine.Replace("\r\n", "");

                string[] keyValue = headerLine.Split(": ");
                if (keyValue.Length == 2)
                    headers.Add(keyValue.First(), keyValue.Last());
            }
            while (!String.IsNullOrEmpty(headerLine));

            return new HttpRequest(info, headers, enumerator);
        }

        public override string ToString()
        {
            return $"{Info}\r\n{Headers}";
        }
    }
}
