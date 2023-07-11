using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetBranchByIdRequest : IRequest<Result<GetBranchByIdResponse>>
    {
        public int Id { get; set; }
    }
}
