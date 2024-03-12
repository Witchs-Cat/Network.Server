using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Execution
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MethodAttribute( string path): Attribute
    {
        public string Path { get; } = path;
    }
}
