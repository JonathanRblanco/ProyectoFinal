using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Clasifications
{
    public class CreateClasificationHandler : IRequestHandler<CreateClasificationRequest, Result>
    {
        private readonly IClasificationsService clasificationsService;

        public CreateClasificationHandler(IClasificationsService clasificationsService)
        {
            this.clasificationsService = clasificationsService;
        }
        public async Task<Result> Handle(CreateClasificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await clasificationsService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
