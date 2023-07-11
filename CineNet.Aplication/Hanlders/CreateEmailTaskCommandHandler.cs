using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using CineNet.Domain.Entities;
using MediatR;

namespace CineNet.Aplication.Hanlders
{
    public class CreateEmailTaskCommandHandler : IRequestHandler<CreateEmailTaskCommand, CreateEmailTaskCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateEmailTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<CreateEmailTaskCommandResponse> Handle(CreateEmailTaskCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.EmailTasksRepository.Create(new EmailTask { Data = request.Data, Type = request.Type, Status = "Pendiente" }, unitOfWork.Transaction);
            return new CreateEmailTaskCommandResponse();
        }
    }
}
