using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "Admin,Usu")]
    public class CinemasController : Controller
    {
        private readonly ILogger<CinemasController> logger;
        private readonly IMediator mediator;

        public CinemasController(ILogger<CinemasController> logger,
            IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetCinemas(GetCinemasRequest request)
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
        public async Task<IActionResult> Create(CreateCinemaRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Cine creado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(GetCinemaByIdRequest request)
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
        public async Task<IActionResult> Edit(ModifyCinemaRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Cine editado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCinemaRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Cine eliminado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public IActionResult Statistics(int CinemaId)
        {
            ViewBag.Cinema = CinemaId;
            return View();
        }
        public async Task<IActionResult> GetStatistics(GetStatisticsRequest request)
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
            catch(Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
    }
}