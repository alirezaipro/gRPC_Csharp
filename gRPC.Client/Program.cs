using gRPC_Client.protos;
using Grpc.Core;

string target = "127.0.0.1:50051";

var channel = new Channel(target, ChannelCredentials.Insecure);

try
{
    await channel.ConnectAsync();

    Console.WriteLine("Connected to gRPC server in gRPC client");

    var productServiceClient = new ProductService.ProductServiceClient(channel);

    #region Create Product

    //CreateProductResponseDto result = productServiceClient.CreateProduct(new CreateProductRequestDto()
    //{
    //    Description = "Desc 1",
    //    Price = 3000,
    //    Title = "Title 1"
    //});

    //Console.WriteLine(result.Message);
    //Console.WriteLine(result.ProductId);

    #endregion

    #region Get product by tag

    var response = productServiceClient.GetProductByTag(new GetProductByTagRequestDto()
    {
        Tag = "Tag 1"
    });


    while (await response.ResponseStream.MoveNext())
    {
        Console.WriteLine($"Product Id : {response.ResponseStream.Current.Id} -- Product Title : {response.ResponseStream.Current.Title}");

        Console.WriteLine("----------------------------------");

       await Task.Delay(1000);
    }

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