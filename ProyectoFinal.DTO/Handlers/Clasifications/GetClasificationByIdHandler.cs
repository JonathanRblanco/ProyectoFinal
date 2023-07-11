using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Clasifications
{
    public class GetClasificationByIdHandler : IRequestHandler<GetClasificationByIdRequest, Result<GetClasificationByIdResponse>>
    {
        private readonly IClasificationsService clasificationsService;

        public GetClasificationByIdHandler(IClasificationsService clasificationsService)
        {
            this.clasificationsService = clasificationsService;
        }
        public async Task<Result<GetClasificationByIdResponse>> Handle(GetClasificationByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var clasification = await clasificationsService.GetById<GetClasificationByIdResponse>(request.Id);
                if (clasification == null)
                {
                    return Result.Error("La clasificacion no existe");
                }
                return Result.Success(clasification);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
