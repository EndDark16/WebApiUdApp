using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiUdApp.Dtos.Request.PublicacionesRequest;
using WebApiUdApp.Dtos.Response.PublicacionesResponse;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicacionesController : ControllerBase
    {
        private readonly PublicacionesRepositorio _publicacionesRepositorio;

        public PublicacionesController(PublicacionesRepositorio publicacionesRepositorio)
        {
            _publicacionesRepositorio = publicacionesRepositorio;
        }

        [HttpPost("reportar")]
        public IActionResult Reportar(ReportarPublicacionRequest request)
        {
            try
            {
                _publicacionesRepositorio.ReportarPublicacion(request.IdPublicacion, DateTime.Now, request.Motivo, request.IdUusuarioReportador);
                return Ok(new ReportarPublicacionResponse
                {
                    Success = true,
                    Message = "Publicación reportada con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ReportarPublicacionResponse
                {
                    Success = false,
                    Message = $"Error al reportar la publicación: {ex.Message}"
                });
            }
        }

        [HttpPost("hacer-publicacion")]
        public IActionResult HacerPublicacion(HacerPublicacionRequest request)
        {
            try
            {
                _publicacionesRepositorio.InsertarPublicacion(request.Titulo, DateTime.Now, request.IdUsuario);
                return Ok(new HacerPublicacionResponse
                {
                    Success = true,
                    Message = "Publicación realizada con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new HacerPublicacionResponse
                {
                    Success = false,
                    Message = $"Error al realizar la publicación: {ex.Message}"
                });
            }
        }

        [HttpPost("toggle-like")]
        public IActionResult ToggleLike(ToggleLikeRequest request)
        {
            try
            {
                if (request.LikeStatus)
                {
                    _publicacionesRepositorio.GuardarLike(request.IdUsuario, request.IdPublicacion);
                }
                else
                {
                    _publicacionesRepositorio.EliminarLike(request.IdUsuario, request.IdPublicacion);
                }

                return Ok(new ToggleLikeResponse
                {
                    Success = true,
                    Message = "Like actualizado con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ToggleLikeResponse
                {
                    Success = false,
                    Message = $"Error al actualizar el like: {ex.Message}"
                });
            }
        }

        [HttpPost("eliminar-publicacion")]
        public IActionResult EliminarPublicacion(EliminarPublicacionRequest request)
        {
            try
            {
                _publicacionesRepositorio.EliminarPublicacion(request.IdPublicacion);
                return Ok(new EliminarPublicacionResponse
                {
                    Success = true,
                    Message = "Publicación eliminada con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EliminarPublicacionResponse
                {
                    Success = false,
                    Message = $"Error al eliminar la publicación: {ex.Message}"
                });
            }
        }

        [HttpPost("eliminar-reporte")]
        public IActionResult EliminarReporte(EliminarReporteRequest request)
        {
            try
            {
                _publicacionesRepositorio.EliminarReporte(request.IdPublicacion);
                return Ok(new EliminarReporteResponse
                {
                    Success = true,
                    Message = "Reporte eliminado con éxito"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new EliminarReporteResponse
                {
                    Success = false,
                    Message = $"Error al eliminar el reporte: {ex.Message}"
                });
            }
        }

        [HttpGet("administrar-reportes")]
        public IActionResult AdministrarReportes()
        {
            var publicacionesReportadas = _publicacionesRepositorio.ObtenerPublicacionesReportadas();
            return Ok(new AdministrarReportesResponse
            {
                PublicacionesReportadas = publicacionesReportadas
            });
        }

        [HttpGet("reporte-publicaciones-pdf")]
        public IActionResult ReportePublicacionesPDF()
        {
            var listPosts = _publicacionesRepositorio.ObtenerPublicacionesOrdenadasPorLikes();
            return Ok(new ReportePublicacionResponse
            {
                Publicaciones = listPosts
            });
        }

        [HttpGet("pagina-principal")]
        [Authorize] // Asegura que solo usuarios autenticados puedan acceder
        public IActionResult PaginaPrincipal()
        {
            try
            {
                // Obtener la identidad del usuario autenticado
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    return Unauthorized(new { message = "Usuario no autenticado" });
                }

                // Obtener el IdUsuario del claim "sub" (subject)
                var userIdClaim = identity.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "El token no contiene un IdUsuario válido" });
                }

                int userId = int.Parse(userIdClaim.Value);

                // Obtener las publicaciones recientes del usuario
                var publicaciones = _publicacionesRepositorio.PublicacionesRecientes(userId);
                if (publicaciones == null || !publicaciones.Any())
                {
                    return NotFound(new { message = "No se encontraron publicaciones para este usuario." });
                }

                // Devolver las publicaciones con un código de estado 200 OK
                return Ok(publicaciones);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error inesperado
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }
    }
}