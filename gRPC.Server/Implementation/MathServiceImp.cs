using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using gRPC_Client.protos;

namespace gRPC.Server.Implementation
{
    public class MathServiceImp:MathService.MathServiceBase
    {
        public override async Task<DivisionResponseDto> CalculateDivision(DivisionRequestDto request, ServerCallContext context)
        {
            if(request.Number== 0)
            {
                throw new RpcException(new Status(StatusCode.Internal, "value cannot equals zero"));
            }

            int result = request.Number / 2;

            return new DivisionResponseDto
            {
                Result= result
            };
        }
    }
}
