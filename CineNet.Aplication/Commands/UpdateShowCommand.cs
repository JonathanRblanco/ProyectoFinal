using MediatR;

namespace CineNet.Aplication.Commands
{
    public class UpdateShowCommand : IRequest<UpdateShowCommandResponse>
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
