using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.UserRequest;
using WebApiUdApp.Dtos.Response.UserResponse;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioServicio _usuarioServicio;

        public UsuarioController(UsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }
        // POST: api/Usuario/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest loginDto)
        {
            try
            {
                var respuesta = _usuarioServicio.IniciarSesion(loginDto);

                if (!respuesta.Exito)
                {
                    return Unauthorized(new LoginResponse
                    {
                        Exito = false,
                        Mensaje = "Credenciales inválidas."
                    });
                }

                // Generar token JWT
                string token = _usuarioServicio.GenerarToken(respuesta.Usuario.IdUsuario);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Inicio de sesión exitoso",
                    Token = token,
                    Usuario = new
                    {
                        Id = respuesta.Usuario.IdUsuario,
                        Email = respuesta.Usuario.Email,
                        Rol = respuesta.Usuario.IdRol
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Exito = false,
                    Mensaje = "Error en el servidor: " + ex.Message
                });
            }
        }
        // POST: api/Usuario/Registro
        [HttpPost("Registro")]
        public IActionResult Registro([FromBody] RegisterRequest registroDto)
        {
            try
            {
                var errores = _usuarioServicio.ComprobarNuevoRegistro(new UserDto
                {
                    Cedula = registroDto.Cedula,
                    Nombre = registroDto.Nombre,
                    Apellido = registroDto.Apellido,
                    Telefono = registroDto.Telefono,
                    Direccion = registroDto.Direccion,
                    Email = registroDto.Email,
                    Contrasena = registroDto.Contrasena
                });

                if (!string.IsNullOrEmpty(errores))
                {
                    return BadRequest(new RegisterResponse
                    {
                        Exito = false,
                        Mensaje = "Errores en el registro: " + errores
                    });
                }

                // Realizar el registro usando directamente RegisterRequest
                var resultado = _usuarioServicio.RegistrarUsuario(registroDto);

                return Ok(new RegisterResponse
                {
                    Exito = resultado.Exito,
                    Mensaje = resultado.Mensaje
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new RegisterResponse
                {
                    Exito = false,
                    Mensaje = "Error en el servidor: " + ex.Message
                });
            }
        }
        [HttpGet("Obtener-usuario-token")]
        [Authorize]
        public IActionResult GetUsuario([FromHeader] string Authorization)
        {
            string token = Authorization.Replace("Bearer ", "");
            var usuario = _usuarioServicio.ObtenerUsuario(token);
            if (usuario == null) return NotFound("Usuario no encontrado.");
            return Ok(usuario);
        }

        [HttpPut("Actualizar-usuario")]
        [Authorize]
        public IActionResult UpdateUsuario([FromHeader] string Authorization, [FromBody] UserDto usuarioDto)
        {
            try
            {
                string token = Authorization.Replace("Bearer ", "");
                var resultado = _usuarioServicio.ActualizarUsuario(token, usuarioDto);

                if (!resultado.Exito)
                {
                    return BadRequest(new ActualizarUsuarioResponse
                    {
                        Exito = false,
                        Mensaje = resultado.Mensaje
                    });
                }

                return Ok(new ActualizarUsuarioResponse
                {
                    Exito = true,
                    Mensaje = resultado.Mensaje,
                    Usuario = resultado.Usuario
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


        [HttpDelete("Eliminar-usuario")]
        [Authorize]
        public IActionResult DeleteUsuario([FromHeader] string Authorization)
        {
            string token = Authorization.Replace("Bearer ", "");
            bool eliminado = _usuarioServicio.EliminarUsuario(token);
            if (!eliminado) return BadRequest("Error al eliminar el usuario.");

            return NoContent();
        }

        // Método auxiliar para extraer el idUsuario desde el token JWT
        private int ObtenerIdUsuarioDesdeToken()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            string sid = claimsIdentity?.FindFirst(ClaimTypes.Sid)?.Value;

            if (string.IsNullOrEmpty(sid))
            {
                throw new UnauthorizedAccessException("Token inválido o expirado.");
            }

            return int.Parse(sid); // Convertimos el claim 'sid' en el idUsuario
        }
    }
}