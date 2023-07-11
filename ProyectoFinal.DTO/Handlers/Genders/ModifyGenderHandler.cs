using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Genders
{
    public class ModifyGenderHandler : IRequestHandler<ModifyGenderRequest, Result>
    {
        private readonly IGendersService gendersService;

        public ModifyGenderHandler(IGendersService gendersService)
        {
            this.gendersService = gendersService;
        }
        public async Task<Result> Handle(ModifyGenderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await gendersService.Update(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
