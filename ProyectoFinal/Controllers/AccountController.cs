using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Data;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.DTO.Responses;

namespace ProyectoFinal.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
          UserManager<User> userManager,
          IMediator mediator,
          IMapper mapper,
          ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["ReturnUrl"] = returnUrl;
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
                    return RedirectToAction("Login");
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
                    return Redirect(request.ReturnUrl);
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
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUri = Url.Action("GoogleAuth", "Account", new { ReturnUrl = returnUrl });
            var properties = new AuthenticationProperties { RedirectUri = redirectUri };
            return Challenge(properties, provider);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Googleauth(ExternalLoginRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Redirect(request.ReturnUrl ?? "/");
                }
                foreach (var e in result.Errors)
                {
                    ModelState.AddModelError("", e);
                }
                foreach (var e in result.ValidationErrors)
                {
                    ModelState.AddModelError("", e.ErrorMessage);
                }
                ViewData["ReturnUrl"] = "/";
                return View("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                ModelState.AddModelError("", "Error interno, intente nuevamente");
                ViewData["ReturnUrl"] = "/";
                return View("Login");
            }
        }
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Login", "Account");
                }
                return RedirectToAction("AccessDenied");
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied");
            }
        }
        [Authorize]
        public IActionResult MyPayments()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> GetPayments(GetPaymentsRequest request)
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
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ReSendReceiptEmail(ReSendReceiptEmailRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Se te enviara la información nuevamente a la brevedad!" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GeneratePDF(ReceiptPdfRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return File(result.Value, "application/pdf");
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            _logger.LogInformation($"Cerrando Sesion..: Usuario: {_userManager.GetUserName(HttpContext.User)}");
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var response = _mapper.Map<SeeProfileResponse>(user);
            return View(response);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(ModifyUserRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Tus datos se han actualizado correctamente!" });
                }
                return Json(new { error = true, mensaje = "No se han podido actualizar los datos" });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [AllowAnonymous]
        public IActionResult ValidateChangePassword()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ValidateChangePassword(ValidateChangePasswordRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Se te enviara un email con las instrucciones para poder reestablecer tu contraseña" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [AllowAnonymous]
        public IActionResult ChangePassword(string UserId, string Token)
        {
            ViewBag.UserId = UserId;
            ViewBag.Token = Token;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Tu contraseña se ha actualizado con exito" });
                }
                return Json(new { error = true, mensaje = string.Join(", ", result.Errors) + string.Join(", ", result.ValidationErrors.Select(e => e.ErrorMessage)) });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, mensaje = ex.Message });
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangeImage(ChangeImageRequest request)
        {
            try
            {
                var result = await _mediator.Send(request);
                if (result.IsSuccess)
                {
                    return File(result.Value, "image/jpeg");
                }
                return Json(null);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return Json(null);
            }
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
