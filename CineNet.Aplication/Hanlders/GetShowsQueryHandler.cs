using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetShowsQueryHandler : IRequestHandler<GetShowsQuery, IEnumerable<GetShowsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetShowsQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<GetShowsQueryResponse>> Handle(GetShowsQuery request, CancellationToken cancellationToken)
        {
            var shows = await unitOfWork.ShowsRepository.GetAllAsync(unitOfWork.Transaction);
            foreach (var show in shows)
            {
                show.Movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(show.MovieId, unitOfWork.Transaction);
                show.Movie.Gender = await unitOfWork.GendersRepository.GetById(show.Movie.GenderId, unitOfWork.Transaction);
                show.Movie.Clasification = await unitOfWork.ClasificationsRepository.GetById(show.Movie.ClasificationId, unitOfWork.Transaction);
                show.ShowType = await unitOfWork.ShowsTypeRepository.GetById(show.ShowTypeId, unitOfWork.Transaction);
                show.Room = await unitOfWork.RoomsRepository.GetById(show.RoomId, unitOfWork.Transaction);
                show.Room.CinemaBranch = await unitOfWork.BranchesRepository.GetById(show.Room.CinemaBranchId, unitOfWork.Transaction);
                show.Room.CinemaBranch.Cinema = await unitOfWork.CinemasRepository.GetById(show.Room.CinemaBranch.CineId, unitOfWork.Transaction);
            }
            return mapper.Map<IEnumerable<GetShowsQueryResponse>>(shows);
        }
    }
}
