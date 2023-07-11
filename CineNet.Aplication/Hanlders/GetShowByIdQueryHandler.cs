using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetShowByIdQueryHandler : IRequestHandler<GetShowByIdQuery, GetShowByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetShowByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetShowByIdQueryResponse> Handle(GetShowByIdQuery request, CancellationToken cancellationToken)
        {
            var show = await unitOfWork.ShowsRepository.GetById(request.Id, unitOfWork.Transaction);
            show.Movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(show.MovieId, unitOfWork.Transaction);
            show.Movie.Gender = await unitOfWork.GendersRepository.GetById(show.Movie.GenderId, unitOfWork.Transaction);
            show.Movie.Clasification = await unitOfWork.ClasificationsRepository.GetById(show.Movie.ClasificationId, unitOfWork.Transaction);
            show.ShowType = await unitOfWork.ShowsTypeRepository.GetById(show.ShowTypeId, unitOfWork.Transaction);
            show.Room = await unitOfWork.RoomsRepository.GetById(show.RoomId, unitOfWork.Transaction);
            show.Room.CinemaBranch = await unitOfWork.BranchesRepository.GetById(show.Room.CinemaBranchId, unitOfWork.Transaction);
            show.Room.CinemaBranch.Cinema = await unitOfWork.CinemasRepository.GetById(show.Room.CinemaBranch.CineId, unitOfWork.Transaction);
            return mapper.Map<GetShowByIdQueryResponse>(show);
        }
    }
}
