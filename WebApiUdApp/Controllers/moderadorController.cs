using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.PublicacionesRequest;
using WebApiUdApp.Dtos.Request.ReporteRequest;
using WebApiUdApp.Dtos.Response.ModeradorResponse;
using WebApiUdApp.Repositories;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModeradorController : ControllerBase
    {
        private readonly ModeradorService _moderadorService;

        public ModeradorController(ModeradorService moderadorService)
        {
            _moderadorService = moderadorService;
        }

        [HttpGet("listar-publicaciones-reportadas")]
        [Authorize(Roles = "Moderador")]
        public IActionResult ListarPublicacionesReportadas([FromHeader] string Authorization)
        {
            try
            {
                List<ReporteDto> reportesDto = _moderadorService.ObtenerPublicacionesReportadas();
                if (reportesDto != null)
                {
                    return Ok(new ListarReportadasResponse
                    {
                        Exito = true,
                        Mensaje = "Publicaciones reportadas obtenidas con exito",
                        ReportesDto = reportesDto
                    });
                }
                else
                {
                    return Ok(new ListarReportadasResponse
                    {
                        Exito = true,
                        Mensaje = "No se encontraron publicaciones reportadas",
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener publicaciones reportadas.", error = ex.Message });
            }
        }
        [HttpDelete("eliminar-reporte-mod")]
        [Authorize (Roles = "Moderador")]
        public IActionResult EliminarReporte(int idReporte)
        {
            try
            {
                bool resultado = _moderadorService.EliminarReporteMod(idReporte);
                if (resultado)
                {
                    return Ok(new EliminarReporteResponse
                    {
                        Exito = resultado,
                        Mensaje = "Reporte eliminado con exito"
                    });
                }
                else
                {
                    return BadRequest(new EliminarReporteResponse
                    {
                        Exito = false,
                        Mensaje = "Error al eliminar el reporte"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EliminarReporteResponse{ 
                    Exito = false,
                    Mensaje = "Error al eliminar el reporte." + ex.Message });
            }
        }
        [HttpDelete("eliminar-publicacion-mod")]
        [Authorize(Roles = "Moderador")]
        public IActionResult EliminarPublicacionMod(int idReporte)
        {
            try
            {
                bool resultado = _moderadorService.EliminarPublicacionReportada(idReporte);
                if (resultado)
                {
                    return Ok(new EliminarReporteResponse
                    {
                        Exito = resultado,
                        Mensaje = "Publicacion eliminada con exito"
                    });
                }
                else
                {
                    return BadRequest(new EliminarReporteResponse
                    {
                        Exito = false,
                        Mensaje = "Error al eliminar la publicacion"
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EliminarReporteResponse
                {
                    Exito = false,
                    Mensaje = "Error al eliminar la publicacion." + ex.Message
                });
            }
        }
    }
}