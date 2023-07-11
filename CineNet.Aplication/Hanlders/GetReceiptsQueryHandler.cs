using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, IEnumerable<GetReceiptsQueryResponse>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetReceiptsQueryHandler(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<GetReceiptsQueryResponse>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
        {
            var receipts = await unitOfWork.ReceiptsRepository.GetAllAsync(unitOfWork.Transaction);
            foreach (var item in receipts)
            {
                item.Show = await unitOfWork.ShowsRepository.GetById(item.ShowId, unitOfWork.Transaction);
                item.Show.ShowType = await unitOfWork.ShowsTypeRepository.GetById(item.Show.ShowTypeId, unitOfWork.Transaction);
                item.Show.Movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(item.Show.MovieId, unitOfWork.Transaction);
                item.Show.Room = await unitOfWork.RoomsRepository.GetById(item.Show.RoomId, unitOfWork.Transaction);
                item.Show.Room.CinemaBranch = await unitOfWork.BranchesRepository.GetById(item.Show.Room.CinemaBranchId, unitOfWork.Transaction);
                item.Show.Room.CinemaBranch.Cinema = await unitOfWork.CinemasRepository.GetById(item.Show.Room.CinemaBranch.CineId, unitOfWork.Transaction);
            }
            return mapper.Map<IEnumerable<GetReceiptsQueryResponse>>(receipts);
        }
    }
}
