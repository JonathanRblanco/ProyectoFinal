using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Clasifications
{
    public class ModifyClasificationHandler : IRequestHandler<ModifyClasificationRequest, Result>
    {
        private readonly IClasificationsService clasificationsService;

        public ModifyClasificationHandler(IClasificationsService clasificationsService)
        {
            this.clasificationsService = clasificationsService;
        }
        public async Task<Result> Handle(ModifyClasificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await clasificationsService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
