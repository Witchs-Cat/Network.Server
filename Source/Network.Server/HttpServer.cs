using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Network.Server.Handlers;

namespace Network.Server
{
    public class HttpServer
    {
        private readonly ServerConfig _config;
        private readonly Socket _tcpLisener;
        private readonly IRequestHandler _requestHandler;
        private readonly ILogger? _logger;
        private CancellationTokenSource _cancelTokenSource;


        public HttpServer(ServerConfig config, IRequestHandler handler, ILogger? logger = null)
        {
            _config = config;
            _requestHandler = handler;
            _logger = logger;
            _cancelTokenSource = new CancellationTokenSource();
            _tcpLisener = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IsRunning = false;
        }

        public bool IsRunning { get; private set; }


        public async Task StartAsync(CancellationToken token = default) 
        {
            if (IsRunning)
                return;
           
            token.Register(() => StopAsync());
            token = _cancelTokenSource.Token;

            IsRunning = true;

            _tcpLisener.Bind(_config.EndPoint);
            _tcpLisener.Listen(_config.Backlog);

            while (!token.IsCancellationRequested)
            {
                Socket tcpClient = await _tcpLisener.AcceptAsync(token).ConfigureAwait(false);

                _ = _requestHandler.ProcessingAsync(tcpClient)
                    .ContinueWith(OnProcessed);
            }
        }

        private void OnProcessed(Task task)
        {
            if (task.IsFaulted)
                _logger?.LogDebug(task.Exception, "faulted");
            else
                _logger?.LogDebug("successful");
        }

        public Task StopAsync() 
        {
            _cancelTokenSource.Cancel();
            IsRunning = false;
            return Task.CompletedTask;
        }
    }
}
