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

    //var response = productServiceClient.GetProductByTag(new GetProductByTagRequestDto()
    //{
    //    Tag = "Tag 1"
    //});


    //while (await response.ResponseStream.MoveNext())
    //{
    //    Console.WriteLine($"Product Id : {response.ResponseStream.Current.Id} -- Product Title : {response.ResponseStream.Current.Title}");

    //    Console.WriteLine("----------------------------------");

    //   await Task.Delay(1000);
    //}

    #endregion

    #region Update product

    //var client = productServiceClient.UpdateProduct();

    //foreach (var item in Enumerable.Range(1, 10))
    //{
    //    await client.RequestStream.WriteAsync(new UpdateProductRequestDto()
    //    {
    //        Title = $"Title {item}",
    //        Description = $"Description {item}"
    //    });
    //}

    //await client.RequestStream.CompleteAsync();

    //var response = await client.ResponseAsync;

    //Console.WriteLine($"respone message : {response.Message} response status code : {response.Status}");

    #endregion

    #region Get product by id

    var client = productServiceClient.GetProducyById();

    foreach(var item in Enumerable.Range(10, 20))
    {
        await client.RequestStream.WriteAsync(new GetProductByIdRequestDto()
        {
            ProductId = item
        });

        Thread.Sleep(1000);
    }

    await client.RequestStream.CompleteAsync();

    while (await client.ResponseStream.MoveNext())
    {
        string result = $"{client.ResponseStream.Current.ProductResult} => Recive from server";
        Console.WriteLine(result);
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