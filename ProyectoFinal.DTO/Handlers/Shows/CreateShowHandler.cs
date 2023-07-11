using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Shows
{
    public class CreateShowHandler : IRequestHandler<CreateShowRequest, Result>
    {
        private readonly IShowsService showsService;

        public CreateShowHandler(IShowsService showsService)
        {
            this.showsService = showsService;
        }
        public async Task<Result> Handle(CreateShowRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await showsService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
