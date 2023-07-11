using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "Admin,UsuExt")]
    public class ShowsTypeController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<ShowsTypeController> logger;
        public ShowsTypeController(IMediator mediator,
            ILogger<ShowsTypeController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Get(GetShowsTypeRequest request)
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
        public async Task<IActionResult> Create(CreateShowTypeRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Tipo de Funcion creado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(GetShowTypeByIdRequest request)
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
        public async Task<IActionResult> Edit(ModifyShowTypeRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Tipo de Funcion editado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteShowTypeRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Tipo de Funcion eliminado con exito" });
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
