using Microsoft.Extensions.Logging;
using Network.Server;
using Network.Server.Execution;
using Network.Server.Handlers;


using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());

RequestExecutor executor = new();



RequestHandler handler = new(executor);
ServerConfig config = new ServerConfig();
HttpServer server = new(config, handler, factory.CreateLogger<HttpServer>());


await server.StartAsync();