using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.ShowsType
{
    public class GetShowTypeByIdHandler : IRequestHandler<GetShowTypeByIdRequest, Result<GetShowsTypeByIdResponse>>
    {
        private readonly IShowsTypeService showsTypeService;

        public GetShowTypeByIdHandler(IShowsTypeService showsTypeService)
        {
            this.showsTypeService = showsTypeService;
        }
        public async Task<Result<GetShowsTypeByIdResponse>> Handle(GetShowTypeByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var gender = await showsTypeService.GetById<GetShowsTypeByIdResponse>(request.Id);
                if (gender == null)
                {
                    return Result.Error("El tipo de funcion no existe");
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
