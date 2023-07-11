using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.CheckOuts
{
    public class PaymentHandler : IRequestHandler<PaymentRequest, Result>
    {
        private readonly ICheckOutService checkOutService;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public PaymentHandler(ICheckOutService checkOutService,
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor)
        {
            this.checkOutService = checkOutService;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }
        public async Task<Result> Handle(PaymentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userManager.GetUserAsync(contextAccessor.HttpContext.User);
                request.UserId = user.Id;
                var json = JsonConvert.SerializeObject(request);
                await checkOutService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }

    }
}
