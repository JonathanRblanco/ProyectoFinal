using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;
using ProyectoFinal.ServicesContracts;

namespace ProyectoFinal.Controllers
/*La propiedad Authorize en los controladores se utiliza para restringir
 * el acceso a los controladores y acciones en función de la identidad del
 * usuario autenticado. Si un usuario intenta acceder a un controlador o
 * acción que requiere autenticación, ASP.NET Core lo redireccionará a la 
 * página de inicio de sesión y solicitará las credenciales de autenticación.*/
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator mediator;
        private readonly IQRService qrservice;

        public HomeController(ILogger<HomeController> logger,
            IMediator mediator, IQRService qrservice)
        {
            _logger = logger;
            this.mediator = mediator;
            this.qrservice = qrservice;
        }
        public async Task<IActionResult> Index(string Name)
        {
            try
            {
                var result = await mediator.Send(new GetMoviesRequest() { OnlyWithShows = true, Name = Name });
                if (result.IsSuccess)
                {
                    ViewBag.busqueda = Name;
                    return View(result.Value);
                }
                return View("NotFound");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error : {0} {1}", ex.Message, ex.StackTrace);
                return View("NotFound");
            }
        }
    }
}