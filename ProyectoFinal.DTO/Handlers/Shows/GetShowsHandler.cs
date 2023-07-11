using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Shows
{
    public class GetShowsHandler : IRequestHandler<GetShowsRequest, Result<IEnumerable<GetShowsResponse>>>
    {
        private readonly IShowsService showsService;

        public GetShowsHandler(IShowsService showsService)
        {
            this.showsService = showsService;
        }
        public async Task<Result<IEnumerable<GetShowsResponse>>> Handle(GetShowsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var shows = await showsService.GetAll<IEnumerable<GetShowsResponse>>();
                if (request.CinemaId.HasValue)
                {
                    shows = shows.Where(s => s.Room.CinemaBranch.CineId == request.CinemaId.Value);
                }
                if (request.MovieId != 0)
                {
                    shows = shows.Where(s => s.MovieId == request.MovieId);
                }
                if (request.BranchId != 0)
                {
                    shows = shows.Where(s => s.Room.CinemaBranch.Id == request.BranchId);
                }
                if (request.OnlyValid)
                {
                    shows = shows.Where(s => s.Date.DateTime > DateTime.Now && s.Capacity > 0);
                }
                return Result.Success(shows);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
