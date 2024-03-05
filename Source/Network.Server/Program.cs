using Microsoft.Extensions.Logging;
using Network.Server;
using Network.Server.Handlers;


using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());

ServerConfig config = new ServerConfig();
RequestHandler handler = new();
HttpServer server = new(config, handler, factory.CreateLogger<HttpServer>());


await server.StartAsync();
//using Socket listener = new(
//    AddressFamily.InterNetwork,
//    SocketType.Stream,
//    ProtocolType.Tcp);

//string hostName = Dns.GetHostName(); // Retrive the Name of HOST
//// Get the IP
//IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

//IPAddress address = hostEntry.AddressList.First();
//IPEndPoint endPoint = new(IPAddress.Any, 8080);

//Console.WriteLine(address);

//listener.Bind(endPoint);
//listener.Listen(100);

//while (true)
//{ 
//    var handler = await listener.AcceptAsync();
//    _ =Task.Factory.StartNew(async () =>
//    {
//        while (true)
//        {
//            try
//            {
//                var buffer = new byte[1_024];
//                var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
//                var response = Encoding.UTF8.GetString(buffer, 0, received);
//                Console.WriteLine("request: {0}", response);
//                var ackMessage = "HTTP/1.1 200 OK\r\nServer: burmalda/0.0.1\r\nDate: Sat, 08 Mar 2014 22:53:46 GMT\r\nContent-Type: text/html\r\nContent-Length: 7\r\nLast-Modified: Sat, 08 Mar 2014 22:53:30 GMT\r\nConnection: keep-alive\r\nAccept-Ranges: bytes\r\n";
//                var echoBytes = Encoding.UTF8.GetBytes(ackMessage);
//                await handler.SendAsync(echoBytes, 0);
//                Console.WriteLine("responce {0}", ackMessage);
//                await handler.DisconnectAsync(true);
//            }
//            catch (SocketException exception)
//            {
//                Console.WriteLine(exception);
//                break;
//            }

//        }
//    });
//}