using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class AssignCinemaRequest : IRequest<Result<AssignCinemaResponse>>
    {
        public string UserId { get; set; }
    }
}
