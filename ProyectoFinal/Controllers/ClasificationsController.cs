using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ClasificationsController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<ClasificationsController> logger;
        public ClasificationsController(IMediator mediator,
            ILogger<ClasificationsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Get(GetClasificationsRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(result.Value);
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClasificationRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Clasificacion creada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(GetClasificationByIdRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return PartialView("Edit", result.Value);
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ModifyClasificationRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Clasificacion editada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteClasificationRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Clasificacion eliminada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }

        }
    }
}
