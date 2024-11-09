using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.ComentarioRequest;
using WebApiUdApp.Dtos.Response.ComentarioResponse;
using WebApiUdApp.Dtos.Response.UserResponse;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly ComentarioService _comentarioService;

        /// <summary>
        /// Constructor del controlador de Comentario.
        /// </summary>
        public ComentarioController()
        {
            _comentarioService = new ComentarioService();
        }

        /// <summary>
        /// Obtiene todos los comentarios asociados a una publicación específica.
        /// </summary>
        /// <param name="idPublicacion">ID de la publicación</param>
        /// <returns>Lista de comentarios de la publicación especificada.</returns>
        /// <response code="200">Devuelve la lista de comentarios.</response>
        /// <response code="404">Si no se encuentran comentarios para la publicación.</response>
        [HttpGet("publicacion/{idPublicacion}")]
        public ActionResult<List<ComentarioDto>> GetComentariosPorPublicacion(int idPublicacion)
        {
            var comentarios = _comentarioService.ObtenerComentariosPorPublicacion(idPublicacion);

            if (comentarios == null || !comentarios.Any())
            {
                return NotFound("No se encontraron comentarios para esta publicación.");
            }

            return Ok(comentarios);
        }

        /// <summary>
        /// Obtiene un comentario específico por su ID.
        /// </summary>
        /// <param name="idComentario">ID del comentario</param>
        /// <returns>Comentario correspondiente al ID especificado.</returns>
        /// <response code="200">Devuelve el comentario solicitado.</response>
        /// <response code="404">Si el comentario no se encuentra.</response>
        [HttpGet("obtener-comentario")]
        public ActionResult<ComentarioDto> GetComentarioPorId(int idComentario)
        {
            var comentario = _comentarioService.ObtenerComentarioPorId(idComentario);
            if (comentario == null)
            {
                return NotFound("Comentario no encontrado.");
            }
            return Ok(comentario);
        }

        /// <summary>
        /// Crea un nuevo comentario.
        /// </summary>
        /// <param name="comentarioDto">Objeto ComentarioDto que contiene los datos del nuevo comentario.</param>
        /// <returns>ID del comentario creado.</returns>
        /// <response code="201">Comentario creado exitosamente.</response>
        /// <response code="400">Si el objeto ComentarioDto es nulo o contiene datos inválidos.</response>
        [HttpPost("crear-comentario")]
        [Authorize]
        public IActionResult CrearComentario([FromHeader] string Authorization, CrearComentarioRequest request)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                if (request == null)
                {
                    return BadRequest("El comentario no puede ser nulo.");
                }
                var resultado = _comentarioService.AgregarComentario(token, request);
                if (!resultado.Exito)
                {
                    return BadRequest(new CrearComentarioResponse
                    {
                        Exito = false,
                        Mensaje = resultado.Mensaje
                    });
                }

                return Ok(new CrearComentarioResponse
                {
                    Exito = true,
                    Mensaje = resultado.Mensaje,
                    Comentario = resultado.Comentario
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActualizarUsuarioResponse
                {
                    Exito = false,
                    Mensaje = "Error en el servidor: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Actualiza un comentario existente.
        /// </summary>
        /// <param name="idComentario">ID del comentario a actualizar.</param>
        /// <param name="comentarioDto">Objeto ComentarioDto que contiene los datos actualizados del comentario.</param>
        /// <returns>Código de estado que indica el resultado de la operación.</returns>
        /// <response code="204">Comentario actualizado exitosamente.</response>
        /// <response code="400">Si el objeto ComentarioDto es nulo o contiene datos inválidos.</response>
        /// <response code="404">Si el comentario no se encuentra.</response>
        [HttpPut("actualizar-comentario")]
        [Authorize]
        public ActionResult ActualizarComentario([FromHeader] string Authorization, ActualizarComentarioRequest comentarioActualizado)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                if (comentarioActualizado == null)
                {
                    return BadRequest(new ActualizarComentarioResponse
                    {
                        Success = false,
                        Message = "El comentario no puede ser nulo"
                    });
                }
                var actualizado = _comentarioService.ActualizarComentario(token, comentarioActualizado);
                if (!actualizado.Success)
                {
                    return BadRequest(new ActualizarComentarioResponse
                    {
                        Success = false,
                        Message = "El comentario no puede ser nulo"
                    });
                }
                return Ok(new ActualizarComentarioResponse
                {
                    Success = true,
                    Message = "Comentario actualizado exitosamente"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ActualizarUsuarioResponse
                {
                    Exito = false,
                    Mensaje = "Error en el servidor: " + ex.Message
                });
            }
        }

        /// <summary>
        /// Elimina un comentario específico.
        /// </summary>
        /// <param name="idComentario">ID del comentario a eliminar.</param>
        /// <returns>Código de estado que indica el resultado de la operación.</returns>
        /// <response code="204">Comentario eliminado exitosamente.</response>
        /// <response code="404">Si el comentario no se encuentra.</response>
        [HttpDelete("eliminar-comentario")]
        [Authorize]
        public ActionResult EliminarComentario(int idComentario)
        {
            bool eliminado = _comentarioService.EliminarComentario(idComentario);
            if (eliminado)
            {
                return NoContent();
            }
            return NotFound("Comentario no encontrado o no se pudo eliminar.");
        }

        /// <summary>
        /// Obtiene todos los comentarios de un usuario específico.
        /// </summary>
        /// <param name="idUsuario">ID del usuario</param>
        /// <returns>Lista de comentarios realizados por el usuario especificado.</returns>
        /// <response code="200">Devuelve la lista de comentarios.</response>
        /// <response code="404">Si no se encuentran comentarios para el usuario.</response>
        [HttpGet("comentarios-por-usuario")]
        [Authorize]
        public ActionResult<List<ComentarioDto>> GetComentariosPorUsuario(int idUsuario)
        {
            var comentarios = _comentarioService.ObtenerComentariosPorUsuario(idUsuario);
            if (comentarios == null || comentarios.Count == 0)
            {
                return NotFound("No se encontraron comentarios para este usuario.");
            }
            return Ok(comentarios);
        }
    }
}
