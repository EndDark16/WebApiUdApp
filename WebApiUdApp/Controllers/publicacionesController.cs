using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.PublicacionesRequest;
using WebApiUdApp.Dtos.Response.PublicacionesResponse;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublicacionesController : ControllerBase
    {
        private readonly PublicacionesService _publicacionesService;

        public PublicacionesController(PublicacionesService publicacionesService)
        {
            _publicacionesService = publicacionesService;
        }

        [HttpPost("reportar")]
        [Authorize]
        public IActionResult Reportar([FromHeader] string Authorization, ReportarPublicacionRequest request)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                _publicacionesService.ReportarPublicacion(token, request);
                return Ok(new ReportarPublicacionResponse { Success = true, Message = "Publicación reportada con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ReportarPublicacionResponse { Success = false, Message = $"Error al reportar la publicación: {ex.Message}" });
            }
        }

        [HttpPost("hacer-publicacion")]
        [Authorize]
        public IActionResult HacerPublicacion([FromHeader] string Authorization, HacerPublicacionRequest request)
        { 
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                    _publicacionesService.HacerPublicacion(token, request);
                return Ok(new HacerPublicacionResponse { Success = true, Message = "Publicación realizada con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new HacerPublicacionResponse { Success = false, Message = $"Error al realizar la publicación: {ex.Message}" });
            }
        }
        [HttpGet("publicacion-por-id")]
        public IActionResult ObtenerPublicacion(int id)
        {
            try
            {
                var publicacion = _publicacionesService.ObtenerPublicacionPorId(id);
                if (publicacion == null)
                {
                    return NotFound("Publicación no encontrada");
                }
                return Ok(publicacion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }

        [HttpPut("actualizar-publicacion")]
        [Authorize]
        public IActionResult ActualizarPublicacion([FromHeader] string Authorization, ActualizarPublicacionRequest request)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                _publicacionesService.ActualizarPublicacion(token, request);
                return Ok(new ActualizarPublicacionResponse { Success = true, Message = "Publicación actualizada con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActualizarPublicacionResponse { Success = false, Message = $"Error al realizar la publicación: {ex.Message}" });
            }
        }

        [HttpDelete("eliminar-publicacion")]
        [Authorize]
        public IActionResult EliminarPublicacion([FromHeader] string Authorization, int idPublicacion)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                bool eliminado = _publicacionesService.EliminarPublicacion(token, idPublicacion);
                if (!eliminado)
                {
                    return StatusCode(500, "Error al eliminar la publicación");
                }
                return Ok("Publicación eliminada con éxito");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno: " + ex.Message);
            }
        }
        [HttpPost("toggle-like")]
        public IActionResult ToggleLike(ToggleLikeRequest request)
        {
            try
            {
                _publicacionesService.ToggleLike(request);
                return Ok(new ToggleLikeResponse { Success = true, Message = "Like actualizado con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ToggleLikeResponse { Success = false, Message = $"Error al actualizar el like: {ex.Message}" });
            }
        }

        [HttpGet("pagina-principal")]
        [Authorize]
        public IActionResult PaginaPrincipal()
        {
            try
            {
                var publicaciones = _publicacionesService.ObtenerPublicacionesRecientes(User);
                if (!publicaciones.Any())
                {
                    return NotFound(new { message = "No se encontraron publicaciones para este usuario." });
                }
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}