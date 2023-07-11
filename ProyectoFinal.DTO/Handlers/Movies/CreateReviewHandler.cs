using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;


namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewRequest, Result>
    {
        private readonly IReviewsService reviewsService;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor contextAccessor;

        public CreateReviewHandler(IReviewsService reviewsService,
                UserManager<User> userManager,
                IHttpContextAccessor contextAccessor)
        {
            this.reviewsService = reviewsService;
            this.userManager = userManager;
            this.contextAccessor = contextAccessor;
        }
        public async Task<Result> Handle(CreateReviewRequest request, CancellationToken cancellation)
        {
            try
            {
                request.UserId = userManager.GetUserId(contextAccessor.HttpContext.User);
                var json = JsonConvert.SerializeObject(request);
                await reviewsService.Create(json);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Error($"Error! : {ex.Message}");
            }
        }
    }
}
