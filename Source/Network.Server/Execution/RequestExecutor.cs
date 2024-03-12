using Microsoft.Extensions.DependencyInjection;
using Network.Server.Handlers;
using Network.Server.Request;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Network.Server.Execution
{
    public class RequestExecutor
    {
        private readonly Dictionary<string, Performer> _performers;

        public RequestExecutor()
        {
            //_services = services;
            _performers = new();
        }

        public void AddPerformer(string path, Performer performer) 
        {
            _performers.Add(path, performer);
        }

        public async Task ExecuteAsync(HttpContext context)
        {
            if (_performers.TryGetValue(context.Request.Info.Path, out Performer? performer))
            {
                await performer.ExecuteAsync(context);
            }
        }
    }
}