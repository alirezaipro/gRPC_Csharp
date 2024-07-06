using gRPC_Client.protos;
using Grpc.Core;

namespace gRPC.Server.Implementation;

public class ProductServiceImp : ProductService.ProductServiceBase
{
    public override Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto request,
        ServerCallContext context)
    {
        return Task.FromResult(new CreateProductResponseDto()
        {
            Message = "Create Product Successfully done",
            ProductId = 20
        });
    }

    public override async Task GetProductByTag(GetProductByTagRequestDto request, IServerStreamWriter<GetProductByTagResponseDto> responseStream, ServerCallContext context)
    {
        await Console.Out.WriteLineAsync($"Tag : {request.Tag}");
        foreach (var item in Enumerable.Range(1, 10))
        {
           await responseStream.WriteAsync(new GetProductByTagResponseDto()
            {
                Id = item,
                Title = $"Product - {item}"
            });
        }
    }
}