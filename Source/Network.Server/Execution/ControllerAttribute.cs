using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Server.Execution
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ControllerAttribute(string path) : Attribute
    {
        public string Path { get; } = path;
    }
}
