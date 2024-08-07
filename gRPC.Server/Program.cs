using gRPC_Client.protos;
using Grpc.Core;
using gRPC.Server.Implementation;
using Grpc.Reflection;
using Grpc.Reflection.V1Alpha;

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

    var reflectionServiceImpl = new ReflectionServiceImpl(ProductService.Descriptor);

    server = new Server()
    {
        Services = {
            ProductService.BindService(new ProductServiceImp()),
            MathService.BindService(new MathServiceImp()),
            ServerReflection.BindService(reflectionServiceImpl)
        },
        Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
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