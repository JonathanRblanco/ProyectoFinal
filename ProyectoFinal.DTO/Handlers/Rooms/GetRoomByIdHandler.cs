using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Rooms
{
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdRequest, Result<GetRoomByIdResponse>>
    {
        private readonly IRoomsService roomsService;

        public GetRoomByIdHandler(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }
        public async Task<Result<GetRoomByIdResponse>> Handle(GetRoomByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var room = await roomsService.GetById<GetRoomByIdResponse>(request.Id);
                if (room == null)
                {
                    return Result.Error("La sala no existe");
                }
                return Result.Success(room);
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
