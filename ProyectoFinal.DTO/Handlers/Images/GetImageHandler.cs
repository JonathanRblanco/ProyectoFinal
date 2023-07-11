using Ardalis.Result;
using MediatR;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.DTO.Handlers.Images
{
    public class GetImageHandler : IRequestHandler<GetImageRequest, Result<GetImageResponse>>
    {
        private readonly IImageService imageService;

        public GetImageHandler(IImageService imageService)
        {
            this.imageService = imageService;
        }
        public async Task<Result<GetImageResponse>> Handle(GetImageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var image = await imageService.GetById<GetImageResponse>(request.Id);
                return Result.Success(image);
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
