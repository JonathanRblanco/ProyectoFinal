using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Movies
{
    public class GetReviewsHandler : IRequestHandler<GetReviewsRequest, Result<IEnumerable<GetReviewsResponse>>>
    {
        private readonly IReviewsService reviewsService;
        private readonly UserManager<User> userManager;

        public GetReviewsHandler(IReviewsService reviewsService,
            UserManager<User> userManager)
        {
            this.reviewsService = reviewsService;
            this.userManager = userManager;
        }
        public async Task<Result<IEnumerable<GetReviewsResponse>>> Handle(GetReviewsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var reviews = await reviewsService.GetAll<IEnumerable<GetReviewsResponse>>();
                reviews = reviews.Where(r => r.MovieId == request.MovieId);
                foreach (var review in reviews)
                {
                    review.UserName = (await userManager.FindByIdAsync(review.UserId)).UserName;
                }
                return Result.Success(reviews);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
