using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.ShowsType
{
    public class GetShowsTypeHandler : IRequestHandler<GetShowsTypeRequest, Result<IEnumerable<GetShowsTypeResponse>>>
    {
        private readonly IShowsTypeService showsTypeService;

        public GetShowsTypeHandler(IShowsTypeService showsTypeService)
        {
            this.showsTypeService = showsTypeService;
        }
        public async Task<Result<IEnumerable<GetShowsTypeResponse>>> Handle(GetShowsTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var showsType = await showsTypeService.GetAll<IEnumerable<GetShowsTypeResponse>>();
                return Result.Success(showsType);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
