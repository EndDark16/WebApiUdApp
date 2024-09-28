using Microsoft.AspNetCore.Mvc;
using System;
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
                string token = _usuarioServicio.GenerarToken(respuesta.Usuario.Id);

                return Ok(new
                {
                    Exito = true,
                    Mensaje = "Inicio de sesión exitoso",
                    Token = token,
                    Usuario = new
                    {
                        Id = respuesta.Usuario.Id,
                        Email = respuesta.Usuario.Email,
                        Rol = respuesta.Usuario.idRol
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
    }
}