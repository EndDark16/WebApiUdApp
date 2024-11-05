using Microsoft.AspNetCore.Mvc;
using WebApiUdApp.Dtos.Request.ReporteRequest;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReporteController : ControllerBase
    {
        private readonly ReporteService _reporteService;

        public ReporteController(ReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpPost("enviar-reporte")]
        public async Task<IActionResult> EnviarReporte([FromBody] EnviarReporteRequest request)
        {
            try
            {
                await _reporteService.EnviarCorreoReporteAsync(request.Destinatario, request.NombreReporte);
                return Ok(new { message = "Reporte enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el reporte.", error = ex.Message });
            }
        }
    }
}