using gRPC_Client.protos;
using Grpc.Core;
using gRPC.Server.Implementation;

const int port = 50051;

Server? server = null;

try
{
    server = new Server()
    {
        Services = { ProductService.BindService(new ProductServiceImp()) },
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