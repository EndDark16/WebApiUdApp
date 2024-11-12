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

        [HttpPost("enviar-reporte-reportadas")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> EnviarReporteReportadas([FromHeader] string Authorization)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                await _reporteService.EnviarCorreoReporteAsync(token);
                return Ok(new { message = "Reporte enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el reporte.", error = ex.Message });
            }
        }
        [HttpPost("enviar-reporte-likeadas")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> EnviarReporteLikeadas([FromHeader] string Authorization)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                await _reporteService.EnviarCorreoPopularesAsync(token);
                return Ok(new { message = "Reporte enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el reporte.", error = ex.Message });
            }
        }
        [HttpPost("enviar-reporte-usuarios")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> EnviarReporteUsuarios([FromHeader] string Authorization)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                await _reporteService.EnviarCorreoRegistrosAsync(token);
                return Ok(new { message = "Reporte enviado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al enviar el reporte.", error = ex.Message });
            }
        }
        [HttpGet("descargar-reporte-publicaciones-reportadas")]
        [Authorize(Roles = "Gerente")]
        public IActionResult DescargarReportePublicacionesReportadas()
        {
            try
            {
                byte[] pdfBytes = _reporteService.GenerarReportePublicacionesReportadas();

                // Devolver el PDF como un archivo descargable
                return File(pdfBytes, "application/pdf", "ReportePublicacionesReportadas.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el reporte: " + ex.Message);
                return StatusCode(500, new { message = "Error al generar el reporte." });
            }
        }
        [HttpGet("descargar-reporte-publicaciones-populares")]
        [Authorize(Roles = "Gerente")]
        public IActionResult DescargarReportePublicacionesPopulares()
        {
            try
            {
                byte[] pdfBytes = _reporteService.GenerarReportePublicacionesPopulares();

                // Devolver el PDF como un archivo descargable
                return File(pdfBytes, "application/pdf", "ReportePublicacionesPopulares.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el reporte: " + ex.Message);
                return StatusCode(500, new { message = "Error al generar el reporte." });
            }
        }

        [HttpGet("descargar-reporte-usuarios")]
        [Authorize(Roles = "Gerente")]
        public IActionResult DescargarReporteUsuarios([FromHeader] string Authorization)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                byte[] pdfBytes = _reporteService.GenerarReporteUsuariosPdf(token);

                return File(pdfBytes, "application/pdf", "ReporteUsuariosRegistrados.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al descargar el reporte.", error = ex.Message });
            }
        }
    }
}