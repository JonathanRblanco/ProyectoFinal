using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "UsuExt")]
    public class ShowsController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<ShowsController> logger;
        public ShowsController(IMediator mediator,
            ILogger<ShowsController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Get(GetShowsRequest request)
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
            return PartialView("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShowRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Funcion creada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(GetShowByIdRequest request)
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
        public async Task<IActionResult> Edit(ModifyShowRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Funcion editada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteShowRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Funcion eliminada con exito" });
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
