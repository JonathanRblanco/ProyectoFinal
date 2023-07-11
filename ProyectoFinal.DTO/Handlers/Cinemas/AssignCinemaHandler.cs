using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class AssignCinemaHandler : IRequestHandler<AssignCinemaRequest, Result<AssignCinemaResponse>>
    {
        private readonly ICinemaService cinemaService;

        public AssignCinemaHandler(ICinemaService cinemaService)
        {
            this.cinemaService = cinemaService;
        }
        public async Task<Result<AssignCinemaResponse>> Handle(AssignCinemaRequest request, CancellationToken cancellation)
        {
            try
            {
                var cines = await cinemaService.GetAll<IEnumerable<GetCinemasResponse>>();
                var cinesByUserId = (await cinemaService.GetCinemasByUserId<IEnumerable<GetCinemasResponse>>(request.UserId)).Select(c => c.Id);

                var response = new AssignCinemaResponse();

                response.CinemasList = new MultiSelectList(cines.Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name
                }), "Id", "Name", cinesByUserId);

                return Result.Success(response);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
