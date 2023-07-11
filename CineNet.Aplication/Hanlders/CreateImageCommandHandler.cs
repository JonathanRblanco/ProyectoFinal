using CineNet.Aplication.Commands;
using CineNet.Domain.Contracts;
using MediatR;


namespace CineNet.Aplication.Hanlders
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, CreateImageCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateImageCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<CreateImageCommandResponse> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            using (var binaryReader = new BinaryReader(request.File.OpenReadStream()))
            {
                var imageData = binaryReader.ReadBytes((int)request.File.Length);
                var resultId = await unitOfWork.ImagesRepository.Create(imageData, unitOfWork.Transaction);
                return new CreateImageCommandResponse { Id = resultId };
            }
        }
    }
}
