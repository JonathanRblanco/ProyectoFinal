using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Clasifications
{
    public class DeleteClasificationHandler : IRequestHandler<DeleteClasificationRequest, Result>
    {
        private readonly IClasificationsService clasificationsService;

        public DeleteClasificationHandler(IClasificationsService clasificationsService)
        {
            this.clasificationsService = clasificationsService;
        }
        public async Task<Result> Handle(DeleteClasificationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await clasificationsService.Delete(request.Id);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
