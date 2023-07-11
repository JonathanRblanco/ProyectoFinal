using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Shows
{
    public class DeleteShowHandler : IRequestHandler<DeleteShowRequest, Result>
    {
        private readonly IShowsService showsService;

        public DeleteShowHandler(IShowsService showsService)
        {
            this.showsService = showsService;
        }
        public async Task<Result> Handle(DeleteShowRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await showsService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
