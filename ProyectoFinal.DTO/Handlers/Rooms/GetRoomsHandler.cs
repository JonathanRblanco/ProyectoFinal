using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Rooms
{
    public class GetRoomsHandler : IRequestHandler<GetRoomsRequest, Result<IEnumerable<GetRoomsResponse>>>
    {
        private readonly IRoomsService _roomsService;

        public GetRoomsHandler(IRoomsService roomsService)
        {
            _roomsService = roomsService;
        }
        public async Task<Result<IEnumerable<GetRoomsResponse>>> Handle(GetRoomsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = await _roomsService.GetAll<IEnumerable<GetRoomsResponse>>();
                if (request.BranchId.HasValue)
                {
                    rooms = rooms.Where(r => r.CinemaBranchId == request.BranchId.Value);
                }
                if (request.CinemaId.HasValue)
                {
                    rooms = rooms.Where(r => r.CinemaBranch.CineId == request.CinemaId.Value);
                }
                return Result<IEnumerable<GetRoomsResponse>>.Success(rooms);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
