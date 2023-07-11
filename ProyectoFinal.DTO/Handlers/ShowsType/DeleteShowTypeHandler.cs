using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.ShowsType
{
    public class DeleteShowTypeHandler : IRequestHandler<DeleteShowTypeRequest, Result>
    {
        private readonly IShowsTypeService showsTypeService;

        public DeleteShowTypeHandler(IShowsTypeService showsTypeService)
        {
            this.showsTypeService = showsTypeService;
        }
        public async Task<Result> Handle(DeleteShowTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await showsTypeService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
