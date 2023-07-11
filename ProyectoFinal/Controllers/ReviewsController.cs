using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "UsuExt")]
    public class ReviewsController : Controller
    {
        private readonly ILogger<ReviewsController> _logger;
        private readonly IMediator _mediator;
        public ReviewsController(ILogger<ReviewsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public IActionResult Reviews()
        {
            return PartialView();
        }

        public ActionResult Create()
        {
            return PartialView("Create");
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Comentario creado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetReviews(GetReviewsRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(result.Value);
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
    }
}
