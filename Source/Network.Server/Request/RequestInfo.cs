using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Request
{
    public class RequestInfo(
        string method,
        string protocol,
        string path)
    {
        public string Method { get; } = method;
        public string Path { get; } = path;
        public string Protocol { get; } = protocol;

        public static RequestInfo ParseFromString(string str)
        {
            string[] values = str.Split(' ');

            if (values.Length != 3)
                throw new ArgumentException(nameof (str));

            return new RequestInfo(values[0], values[2], values[1]);
        }

        public override string ToString()
        {
            return $"{Method} {Path} {Protocol}";
        }
    }
}
