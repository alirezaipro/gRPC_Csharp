using Grpc.Core;
using gRPC_Client.protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC.Client
{
    public class Product
    {
        private readonly ProductService.ProductServiceClient productServiceClient;

        public Product(ProductService.ProductServiceClient productServiceClient)
        {
            this.productServiceClient = productServiceClient;
        }

        public void CreateProduct()
        {
            #region Create Product

            CreateProductResponseDto result = productServiceClient.CreateProduct(new CreateProductRequestDto()
            {
                Description = "Desc 1",
                Price = 3000,
                Title = "Title 1"
            });

            Console.WriteLine(result.Message);
            Console.WriteLine(result.ProductId);

            #endregion
        }

        public async Task GetProductByTag()
        {
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
        }

        public async Task UpdateProduct()
        {
            #region Update product

            var client = productServiceClient.UpdateProduct();

            foreach (var item in Enumerable.Range(1, 10))
            {
                await client.RequestStream.WriteAsync(new UpdateProductRequestDto()
                {
                    Title = $"Title {item}",
                    Description = $"Description {item}"
                });
            }

            await client.RequestStream.CompleteAsync();

            var response = await client.ResponseAsync;

            Console.WriteLine($"respone message : {response.Message} response status code : {response.Status}");

            #endregion
        }

        public async Task GetProductById()
        {
            #region Get product by id

            var client = productServiceClient.GetProducyById();

            foreach (var item in Enumerable.Range(10, 20))
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
        }
    }
}
