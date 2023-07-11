using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Genders
{
    public class DeleteGenderHandler : IRequestHandler<DeleteGenderRequest, Result>
    {
        private readonly IGendersService gendersService;

        public DeleteGenderHandler(IGendersService gendersService)
        {
            this.gendersService = gendersService;
        }
        public async Task<Result> Handle(DeleteGenderRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await gendersService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
