using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.DTO.Requests;

namespace ProyectoFinal.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IMediator mediator;

        public CheckOutController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IActionResult Index(StartCheckOutProcessRequest request)
        {
            return View(request);
        }
        public IActionResult Payment()
        {
            return PartialView("PaymentMethod");
        }
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentRequest request)
        {
            try
            {
                var result = await mediator.Send(request);
                if (result.IsSuccess)
                {
                    return Json(new { mensaje = "Compra realizada con exito, a la brevedad recibiras un mail con la informacion de tu compra. Tambien puedes verlo desde la seccion 'Mis Compras'" });
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
