using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Genders
{
    public class CreateGenderHandler : IRequestHandler<CreateGenderRequest, Result>
    {
        private readonly IGendersService gendersService;
        public CreateGenderHandler(IGendersService gendersService)
        {
            this.gendersService = gendersService;
        }
        public async Task<Result> Handle(CreateGenderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                await gendersService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
