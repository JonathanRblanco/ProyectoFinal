using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetMovieByIdHandler : IRequestHandler<GetMovieByIdQuery, GetMovieByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetMovieByIdHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetMovieByIdQueryResponse> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(request.Id, unitOfWork.Transaction);
            movie.Gender = await unitOfWork.GendersRepository.GetById(movie.GenderId, unitOfWork.Transaction);
            movie.Clasification = await unitOfWork.ClasificationsRepository.GetById(movie.ClasificationId, unitOfWork.Transaction);
            return mapper.Map<GetMovieByIdQueryResponse>(movie);
        }
    }
}
