using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Rooms
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomRequest, Result>
    {
        private readonly IRoomsService roomsService;

        public CreateRoomHandler(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }
        public async Task<Result> Handle(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await roomsService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
