using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Genders
{
    public class GetGenderByIdHandler : IRequestHandler<GetGenderByIdRequest, Result<GetGenderByIdReponse>>
    {
        private readonly IGendersService gendersService;

        public GetGenderByIdHandler(IGendersService gendersService)
        {
            this.gendersService = gendersService;
        }
        public async Task<Result<GetGenderByIdReponse>> Handle(GetGenderByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var gender = await gendersService.GetById<GetGenderByIdReponse>(request.Id);
                if (gender == null)
                {
                    return Result.Error("El genero no existe");
                }
                return Result.Success(gender);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
