using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Shows
{
    public class GetShowByIdHandler : IRequestHandler<GetShowByIdRequest, Result<GetShowByIdResponse>>
    {
        private readonly IShowsService showsService;

        public GetShowByIdHandler(IShowsService showsService)
        {
            this.showsService = showsService;
        }
        public async Task<Result<GetShowByIdResponse>> Handle(GetShowByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var show = await showsService.GetById<GetShowByIdResponse>(request.Id);
                if (show == null)
                {
                    return Result.Error("La funcion no existe");
                }
                return Result.Success(show);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
