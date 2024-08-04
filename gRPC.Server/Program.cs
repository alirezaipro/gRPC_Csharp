using gRPC_Client.protos;
using Grpc.Core;
using gRPC.Server.Implementation;

const int port = 50051;

Server? server = null;


var caCrt = File.ReadAllText("ssl/ca.crt");
var serverCrt = File.ReadAllText("ssl/server.crt");
var serverKey = File.ReadAllText("ssl/server.key");

var keyPair = new KeyCertificatePair(serverCrt, serverKey);

var credentials = new SslServerCredentials(new List<KeyCertificatePair>() {
    keyPair
}, caCrt, true);

try
{
    server = new Server()
    {
        Services = {
            ProductService.BindService(new ProductServiceImp()),
            MathService.BindService(new MathServiceImp())
        },
        Ports = { new ServerPort("localhost", port, credentials) }
    };

    server.Start();
    Console.WriteLine($"Server started and listening on port : {port}");

    Console.ReadKey();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
finally
{
    if (server != null)
        server.ShutdownAsync().Wait();
}