using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Models;
using System.Diagnostics;
namespace ProyectoFinal.Controllers
/*La propiedad Authorize en los controladores se utiliza para restringir
 * el acceso a los controladores y acciones en función de la identidad del
 * usuario autenticado. Si un usuario intenta acceder a un controlador o
 * acción que requiere autenticación, ASP.NET Core lo redireccionará a la 
 * página de inicio de sesión y solicitará las credenciales de autenticación.*/
{
    [Authorize(Roles = "Usu")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}