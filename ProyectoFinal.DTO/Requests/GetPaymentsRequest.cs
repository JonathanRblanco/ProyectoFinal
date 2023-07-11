using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetPaymentsRequest : IRequest<Result<IEnumerable<GetPaymentsResponse>>>
    {
        public string UserId { get; set; }
    }
}
