using Microsoft.Extensions.DependencyInjection;
using Network.Server.Handlers;
using System.Reflection;

namespace Network.Server.Execution
{
    public class Performer(
        IServiceProvider services, 
        Type type,
        MethodInfo method)
    {
        private readonly IServiceProvider _serviceProvider = services;
        private readonly Type _type = type;
        private readonly MethodInfo _method = method;

        public async Task ExecuteAsync(HttpContext context) 
        {
            object? obj = _serviceProvider.GetRequiredService(_type);
            object? result = _method.Invoke(obj, new object[]{context});

            if (result is Task taskResult)
                await taskResult;
        }
    }
}
