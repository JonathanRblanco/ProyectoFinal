using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Rooms
{
    public class ModifyRoomHandler : IRequestHandler<ModifyRoomRequest, Result>
    {
        private readonly IRoomsService roomsService;
        public ModifyRoomHandler(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }
        public async Task<Result> Handle(ModifyRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await roomsService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
