using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    public class ImageController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IMediator mediator,
            ILogger<ImageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetImage(GetImageRequest request)
        {
            var userWithNoImage = Path.Combine("~/Imagenes", "UserWithNoImage.svg");
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return File(result.Value.File, "image/jpeg");
                }
                return File(userWithNoImage, "image/svg+xml");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return File(userWithNoImage, "image/svg+xml");
            }
        }
    }
}
