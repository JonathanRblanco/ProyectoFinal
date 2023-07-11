using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Clasifications
{
    public class GetClasificationsHandler : IRequestHandler<GetClasificationsRequest, Result<IEnumerable<GetClasificationsResponse>>>
    {
        private readonly IClasificationsService clasificationsService;

        public GetClasificationsHandler(IClasificationsService clasificationsService)
        {
            this.clasificationsService = clasificationsService;
        }

        public async Task<Result<IEnumerable<GetClasificationsResponse>>> Handle(GetClasificationsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var clasifications = await clasificationsService.GetAll<IEnumerable<GetClasificationsResponse>>();
                return Result.Success(clasifications);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
