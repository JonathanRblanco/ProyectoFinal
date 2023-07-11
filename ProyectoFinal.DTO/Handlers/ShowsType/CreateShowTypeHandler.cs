using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.ShowsType
{
    public class CreateShowTypeHandler : IRequestHandler<CreateShowTypeRequest, Result>
    {
        private readonly IShowsTypeService showsTypeService;

        public CreateShowTypeHandler(IShowsTypeService showsTypeService)
        {
            this.showsTypeService = showsTypeService;
        }
        public async Task<Result> Handle(CreateShowTypeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await showsTypeService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
