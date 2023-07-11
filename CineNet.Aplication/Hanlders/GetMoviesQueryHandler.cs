using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<GetMoviesQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetMoviesQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetMoviesQueryResponse>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await unitOfWork.MoviesRepository.GetAllAsync(unitOfWork.Transaction);
            foreach (var movie in movies)
            {
                movie.Gender = await unitOfWork.GendersRepository.GetById(movie.GenderId, unitOfWork.Transaction);
                movie.Clasification = await unitOfWork.ClasificationsRepository.GetById(movie.ClasificationId, unitOfWork.Transaction);
            }
            var response = mapper.Map<IEnumerable<GetMoviesQueryResponse>>(movies);
            return response;
        }
    }
}
