using AutoMapper;
using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class AssignCinemaByUserIdHandler : IRequestHandler<AssignCinemaByUserIdCommand, AssignCinemaByUserIdCommandResponse>
    {
        public readonly IUnitOfWork _unitOfWork;
        public readonly IMapper _mapper;

        public AssignCinemaByUserIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssignCinemaByUserIdCommandResponse> Handle(AssignCinemaByUserIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.CinemasRepository.DeleteCinemasAssignByUserId(request.UserId, _unitOfWork.Transaction);

                foreach (var c in request.Ids)
                {
                    await _unitOfWork.CinemasRepository.InsertCinemasAssignByUserId(request.UserId, c, _unitOfWork.Transaction);
                }
                _unitOfWork.CommitTransaction();
                return new AssignCinemaByUserIdCommandResponse();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollBack();
                throw;
            }
        }
    }
}
