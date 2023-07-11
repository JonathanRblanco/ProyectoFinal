using Ardalis.Result;
using MediatR;
using Newtonsoft.Json;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.CheckOuts
{
    public class GetPaymentsHandler : IRequestHandler<GetPaymentsRequest, Result<IEnumerable<GetPaymentsResponse>>>
    {
        private readonly ICheckOutService checkOutService;

        public GetPaymentsHandler(ICheckOutService checkOutService)
        {
            this.checkOutService = checkOutService;
        }
        public async Task<Result<IEnumerable<GetPaymentsResponse>>> Handle(GetPaymentsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonConvert.SerializeObject(request);
                var payments = await checkOutService.GetAll<IEnumerable<GetPaymentsResponse>>();
                if (!string.IsNullOrEmpty(request.UserId))
                {
                    payments = payments.Where(p => p.UserId == request.UserId);
                }
                return Result.Success(payments);
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
