using gRPC_Client.protos;
using Grpc.Core;
using gRPC.Client;

string target = "127.0.0.1:50051";

var channel = new Channel(target, ChannelCredentials.Insecure);

try
{
    await channel.ConnectAsync();

    Console.WriteLine("Connected to gRPC server in gRPC client");

    #region Product

    //var productServiceClient = new ProductService.ProductServiceClient(channel);

    //Product product = new Product(productServiceClient);   

    #endregion

    #region Math

    var mathServiceClient = new MathService.MathServiceClient(channel);

    gRPC.Client.Math math = new gRPC.Client.Math(mathServiceClient);
   
    math.Division();

    #endregion


    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
finally
{
    await channel.ShutdownAsync();
}