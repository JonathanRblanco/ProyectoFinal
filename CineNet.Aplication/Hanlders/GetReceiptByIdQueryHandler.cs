using AutoMapper;
using CineNet.Aplication.Queries;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class GetReceiptByIdQueryHandler : IRequestHandler<GetReceiptByIdQuery, GetReceiptByIdQueryResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetReceiptByIdQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<GetReceiptByIdQueryResponse> Handle(GetReceiptByIdQuery request, CancellationToken cancellationToken)
        {
            var receipt = await unitOfWork.ReceiptsRepository.GetById(request.Id, unitOfWork.Transaction);
            receipt.Show = await unitOfWork.ShowsRepository.GetById(receipt.ShowId, unitOfWork.Transaction);
            receipt.Show.ShowType = await unitOfWork.ShowsTypeRepository.GetById(receipt.Show.ShowTypeId, unitOfWork.Transaction);
            receipt.Show.Movie = await unitOfWork.MoviesRepository.GetMovieByIdAsync(receipt.Show.MovieId, unitOfWork.Transaction);
            receipt.Show.Room = await unitOfWork.RoomsRepository.GetById(receipt.Show.RoomId, unitOfWork.Transaction);
            receipt.Show.Room.CinemaBranch = await unitOfWork.BranchesRepository.GetById(receipt.Show.Room.CinemaBranchId, unitOfWork.Transaction);
            receipt.Show.Room.CinemaBranch.Cinema = await unitOfWork.CinemasRepository.GetById(receipt.Show.Room.CinemaBranch.CineId, unitOfWork.Transaction);
            return mapper.Map<GetReceiptByIdQueryResponse>(receipt);
        }
    }
}
