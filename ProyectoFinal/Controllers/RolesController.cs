using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;
using System.Data;

namespace ProyectoFinal.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<Rol> _roleManager;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;
        private readonly IMediator _mediator;
        public RolesController(RoleManager<Rol> roleManager,
            IMapper mapper,
            ILogger<RolesController> logger,
            IMediator mediator)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        // GET: RolesController
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles;
                var response = _mapper.Map<IEnumerable<GetRolesResponse>>(roles);
                return Json(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(new { error = true, mensaje = ex.Message });
            }

        }
        public IActionResult Create()
        {
            return PartialView("Create");
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRolRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Rol creado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        public async Task<IActionResult> Edit(EditRolRequest request)
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
        public async Task<IActionResult> Edit(ModifyRolRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Rol editado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRolRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Rol eliminado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
    }
}
