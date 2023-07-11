using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, DeleteMovieCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteMovieCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<DeleteMovieCommandResponse> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.MoviesRepository.Delete(request.id, unitOfWork.Transaction);
            return new DeleteMovieCommandResponse();
        }
    }
}
