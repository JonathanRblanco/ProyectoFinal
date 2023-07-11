using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class DeleteGenderRequest : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
