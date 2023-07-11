using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, CreateReviewCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CreateReviewCommandResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = mapper.Map<Review>(request);
            review.Date = DateTime.Now;
            review.Id = await unitOfWork.ReviewsRepository.Create(review, unitOfWork.Transaction);
            return mapper.Map<CreateReviewCommandResponse>(review);
        }
    }
}
