using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Handlers.UserHanlders
{
    public class ChangeImageHandler : IRequestHandler<ChangeImageRequest, Result<byte[]>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;

        public ChangeImageHandler(IUnitOfWork unitOfWork,
            IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
        }
        public async Task<Result<byte[]>> Handle(ChangeImageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var imageId = await _imageService.Create<int>(request.image);
                using (var binaryReader = new BinaryReader(request.image.OpenReadStream()))
                {
                    var imageData = binaryReader.ReadBytes((int)request.image.Length);
                    await _unitOfWork.UsersRepository.ChangeImage(imageId, request.UserId, _unitOfWork.Transaction);
                    return Result.Success(imageData);
                }
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
