using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Contracts;
using ProyectoFinal.DTOs.Requests;
using ProyectoFinal.Models;

namespace ProyectoFinal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IMediator mediator,
          IUnitOfWork unitOfWork,
          IMapper mapper,
          ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                var result = await _mediator.Send(request);

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Usuario registrado con exito: Usuario: {result.Value.Id}");
                    _logger.LogInformation($"Iniciando Sesion: Usuario: {result.Value.Id}");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var e in result.ValidationErrors)
                {
                    ModelState.AddModelError("", e.ErrorMessage);
                }
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e);
                }
                return View(request);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                ModelState.AddModelError("", "Error interno, intente nuevamente");
                return View(request);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInUserRequest request)
        {

            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                var result = await _mediator.Send(request);

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Usuario autenticado con exito: Usuario: {request.UserName}");
                    _logger.LogInformation($"Iniciando Sesion: Usuario: {request.UserName}");
                    return RedirectToAction("Index", "Home");
                }
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e);
                }
                foreach (var e in result.ValidationErrors)
                {
                    ModelState.AddModelError("", e.ErrorMessage);
                }
                return View(request);
            }

            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                ModelState.AddModelError("", "Error interno, intente nuevamente");
                return View(request);
            }
        }

        [Authorize]
        public async Task<ActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            _logger.LogInformation($"Cerrando Sesion..: Usuario: {_userManager.GetUserName(HttpContext.User)}");
            return RedirectToAction("Login", "Account");
        }
    }
}
