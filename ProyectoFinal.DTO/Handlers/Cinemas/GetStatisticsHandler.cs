using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class GetStatisticsHandler : IRequestHandler<GetStatisticsRequest, Result<IEnumerable<GetStatisticsResponse>>>
    {
        private readonly ICinemaService cinemaService;

        public GetStatisticsHandler(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }
        public async Task<Result<IEnumerable<GetStatisticsResponse>>> Handle(GetStatisticsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var statistics = await cinemaService.getStatistics<IEnumerable<GetStatisticsResponse>>(request.CinemaId,request.Year,request.BranchId);
                return Result.Success(statistics);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
