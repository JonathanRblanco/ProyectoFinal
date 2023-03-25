using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Contracts;
using ProyectoFinal.DTOs.Commands.Requests;

namespace ProyectoFinal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
          UserManager<IdentityUser> userManager,
          SignInManager<IdentityUser> signInManager,
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
        public async Task<IActionResult> Register(CreateUserCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _mediator.Send(request);

                if (!string.IsNullOrEmpty(result.Id))
                {
                    _logger.LogInformation($"Usuario registrado con exito: Usuario: {result.Id}");
                    await _signInManager.SignInAsync(result, isPersistent: false);
                    _logger.LogInformation($"Iniciando Sesion: Usuario: {result.Id}");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInUserCommandRequest request)
        {

            if (ModelState.IsValid)
            {
                var loginExitoso = await _mediator.Send(request);
                if (loginExitoso)
                {
                    _logger.LogInformation($"Usuario autenticado con exito: Usuario: {request.Nombre}");
                    _logger.LogInformation($"Iniciando Sesion: Usuario: {request.Nombre}");
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("Password", "La contraseña o el usuario no coinciden");
            }
            return View(request);
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
