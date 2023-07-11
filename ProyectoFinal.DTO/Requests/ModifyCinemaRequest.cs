using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyCinemaRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Status { get; set; }
    }
}
