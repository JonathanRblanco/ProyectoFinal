using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.DTO.Requests
{
    public class GetGenderByIdRequest : IRequest<Result<GetGenderByIdReponse>>
    {
        public int Id { get; set; }
    }
}
