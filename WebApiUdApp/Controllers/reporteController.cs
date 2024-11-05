using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> EnviarReporte([FromHeader] string Authorization, [FromBody] EnviarReporteRequest request)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                await _reporteService.EnviarCorreoReporteAsync(token, request.NombreReporte);
                return Ok(new { message = "Reporte enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el reporte.", error = ex.Message });
            }
        }
    }
}