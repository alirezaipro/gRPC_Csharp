using gRPC_Client.protos;
using Grpc.Core;
using gRPC.Client;

string target = "127.0.0.1:50051";

var caCrt = File.ReadAllText("ssl/ca.crt");
var clientCrt = File.ReadAllText("ssl/client.crt");
var clientKey = File.ReadAllText("ssl/client.key");

var channelCredential = new SslCredentials(caCrt, new KeyCertificatePair(clientCrt, clientKey));

var channel = new Channel("localhost", 50051, channelCredential);

try
{
    await channel.ConnectAsync();

    Console.WriteLine("Connected to gRPC server in gRPC client");

    #region Product

    var productServiceClient = new ProductService.ProductServiceClient(channel);

    Product product = new Product(productServiceClient);

    product.CreateProduct();

    #endregion

    #region Math

    //var mathServiceClient = new MathService.MathServiceClient(channel);

    //gRPC.Client.Math math = new gRPC.Client.Math(mathServiceClient);

    //math.Division();

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