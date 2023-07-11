using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Cinemas
{
    public class GetCinemasHandler : IRequestHandler<GetCinemasRequest, Result<IEnumerable<GetCinemasResponse>>>
    {
        private readonly ICinemaService cinemaService;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly UserManager<User> userManager;

        public GetCinemasHandler(ICinemaService cinemaService,
            IHttpContextAccessor contextAccessor,
            UserManager<User> userManager)
        {
            this.cinemaService = cinemaService;
            this.contextAccessor = contextAccessor;
            this.userManager = userManager;
        }
        public async Task<Result<IEnumerable<GetCinemasResponse>>> Handle(GetCinemasRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<GetCinemasResponse> cinemas;
                if (request.ByUser)
                {
                    var userId = userManager.GetUserId(contextAccessor.HttpContext.User);
                    cinemas = await cinemaService.GetCinemasByUserId<IEnumerable<GetCinemasResponse>>(userId);
                }
                else
                {
                    cinemas = await cinemaService.GetAll<IEnumerable<GetCinemasResponse>>();
                }
                if (request.Id > 0)
                {
                    cinemas = cinemas.Where(c => c.Id == request.Id);
                }
                return Result.Success(cinemas);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
