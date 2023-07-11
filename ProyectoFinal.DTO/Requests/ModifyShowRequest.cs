using Ardalis.Result;
using MediatR;

namespace ProyectoFinal.DTO.Requests
{
    public class ModifyShowRequest : IRequest<Result>
    {
        public int Id { get; set; }
        public int ShowTypeId { get; set; }
        public DateTimeOffset Date { get; set; }
        public int Capacity { get; set; }
        public int RoomId { get; set; }
        public int MovieId { get; set; }
        public decimal Price { get; set; }
    }
}
