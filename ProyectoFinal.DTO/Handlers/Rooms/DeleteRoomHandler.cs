using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Rooms
{
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomRequest, Result>
    {
        private readonly IRoomsService roomsService;

        public DeleteRoomHandler(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }
        public async Task<Result> Handle(DeleteRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await roomsService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
