using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Genders
{
    public class GetGendersHandler : IRequestHandler<GetGendersRequest, Result<IEnumerable<GetGendersResponse>>>
    {
        private readonly IGendersService gendersService;

        public GetGendersHandler(IGendersService gendersService)
        {
            this.gendersService = gendersService;
        }

        public async Task<Result<IEnumerable<GetGendersResponse>>> Handle(GetGendersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var genders = await gendersService.GetAll<IEnumerable<GetGendersResponse>>();
                return Result.Success(genders);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
