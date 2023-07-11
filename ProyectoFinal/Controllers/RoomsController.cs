using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "UsuExt")]
    public class RoomsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RoomsController> _logger;
        public RoomsController(IMediator mediator,
            ILogger<RoomsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetRooms(GetRoomsRequest request)
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
        public IActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Sala creada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(GetRoomByIdRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return PartialView("Edit", result.Value);
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ModifyRoomRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Sala editada con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRoomRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Sala eliminada con exito" });
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
