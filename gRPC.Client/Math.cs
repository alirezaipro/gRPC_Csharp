using Grpc.Core;
using gRPC_Client.protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gRPC.Client
{
    public class Math
    {
        private readonly MathService.MathServiceClient mathServiceClient;

        public Math(MathService.MathServiceClient mathServiceClient)
        {
            this.mathServiceClient = mathServiceClient;
        }

        public void Division()
        {
            try
            {
                DivisionResponseDto response = mathServiceClient.CalculateDivision(new DivisionRequestDto()
                {
                    Number = 0
                },deadline:DateTime.UtcNow.AddSeconds(5));

                Console.WriteLine($"Division result = {response.Result}");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"status code : {e.Status.StatusCode}");
                Console.WriteLine($"message : {e.Status.Detail}");
                throw;
            }
        }
    }
}
