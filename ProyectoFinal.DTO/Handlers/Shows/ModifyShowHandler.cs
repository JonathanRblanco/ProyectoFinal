using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Shows
{
    public class ModifyShowHandler : IRequestHandler<ModifyShowRequest, Result>
    {
        private readonly IShowsService showsService;

        public ModifyShowHandler(IShowsService showsService)
        {
            this.showsService = showsService;
        }
        public async Task<Result> Handle(ModifyShowRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await showsService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
