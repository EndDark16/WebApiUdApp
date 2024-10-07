using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebApiUdApp.Repositories;
using WebApiUdApp.Utilities;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.UserRequest;
using WebApiUdApp.Dtos.Response.UserResponse;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Diagnostics;

namespace WebApiUdApp.Services
{
    public class UsuarioServicio
    {

        private readonly IConfiguration _configuration;

        public UsuarioServicio(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerClaveJwt()
        {
            // Acceder a la clave JWT desde appsettings.json
            return _configuration["Jwt:Key"];
        }

        public string GenerarToken(int idUsuario)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, idUsuario.ToString()), // ID del usuario
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ObtenerClaveJwt()));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // O el emisor que estés utilizando
                audience: null, // O la audiencia que estés utilizando
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Expiración del token
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Verifica si hay valores nulos o vacíos
        private bool EsNulo(string valor) => string.IsNullOrEmpty(valor);

        // Verifica si un texto contiene solo letras y espacios
        private bool EsTextoValido(string valor) => Regex.IsMatch(valor, @"^[a-zA-Z\s]+$");

        // Verifica si el valor contiene solo números
        private bool EsNumero(string valor) => Regex.IsMatch(valor, @"^\d+$");

        // Verifica si un correo ya está registrado en la base de datos
        private bool YaRegistrado(string email)
        {
            UserRepositorio userRepo = new UserRepositorio();
            return userRepo.CorreoYaRegistrado(email);
        }

        // Método para comprobar los errores en el registro
        public string ComprobarNuevoRegistro(UserDto registroNuevo)
        {
            List<string> errores = new List<string>();

            if (EsNulo(registroNuevo.Cedula) || EsNulo(registroNuevo.Nombre) ||
                EsNulo(registroNuevo.Apellido) || EsNulo(registroNuevo.Telefono) ||
                EsNulo(registroNuevo.Email) || EsNulo(registroNuevo.Contrasena))
            {
                errores.Add("Debe llenar todos los campos.");
            }
            else
            {
                if (!EsNumero(registroNuevo.Cedula)) errores.Add("La cédula debe contener solo números.");
                if (!EsTextoValido(registroNuevo.Nombre)) errores.Add("El nombre no debe contener números ni caracteres especiales.");
                if (!EsTextoValido(registroNuevo.Apellido)) errores.Add("El apellido no debe contener números ni caracteres especiales.");
                if (!EsNumero(registroNuevo.Telefono)) errores.Add("El teléfono debe contener solo números.");
                if (!registroNuevo.Email.Contains("@ucundinamarca.edu.co")) errores.Add("El email debe contener \"@ucundinamarca.edu.co\".");
                if (registroNuevo.Contrasena.Length < 8) errores.Add("La contraseña debe tener al menos 8 caracteres.");
                if (YaRegistrado(registroNuevo.Email)) errores.Add("El correo ya está registrado.");
            }

            return errores.Count > 0 ? string.Join("\n", errores) : string.Empty;
        }

        // Registro de usuario
        public RegisterResponse RegistrarUsuario(RegisterRequest registroNuevo)
        {
            var errores = ComprobarNuevoRegistro(new UserDto
            {
                Cedula = registroNuevo.Cedula,
                Nombre = registroNuevo.Nombre,
                Apellido = registroNuevo.Apellido,
                Telefono = registroNuevo.Telefono,
                Direccion = registroNuevo.Direccion,
                Email = registroNuevo.Email,
                Contrasena = registroNuevo.Contrasena
            });

            if (!string.IsNullOrEmpty(errores))
            {
                return new RegisterResponse
                {
                    Exito = false,
                    Mensaje = errores
                };
            }

            // Encriptar la contraseña antes de guardarla
            Argon2Encryptation encriptador = new Argon2Encryptation();
            string contrasenaEncriptada = encriptador.EncriptarContrasenaArgon2(registroNuevo.Contrasena);

            UserDto userNew = new UserDto
            {
                Cedula = registroNuevo.Cedula,
                Nombre = registroNuevo.Nombre,
                Apellido = registroNuevo.Apellido,
                Telefono = registroNuevo.Telefono,
                Direccion = registroNuevo.Direccion,
                Email = registroNuevo.Email,
                Contrasena = contrasenaEncriptada
            };

            try
            {
                UserRepositorio userRepo = new UserRepositorio();
                int filasAfectadas = userRepo.Registro(userNew);

                if (filasAfectadas != 0)
                {
                    // Simular envío de correo al registrar
                    EnviarCorreo correo = new EnviarCorreo();
                    _ = correo.CorreoInicioSesion(userNew.Email, userNew.Nombre);

                    return new RegisterResponse
                    {
                        Exito = true,
                        Mensaje = "Registro exitoso"
                    };
                }
                else
                {
                    return new RegisterResponse
                    {
                        Exito = false,
                        Mensaje = "Error al registrar usuario"
                    };
                }
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Exito = false,
                    Mensaje = "Error en el servidor: " + ex.Message
                };
            }
        }

        // Iniciar sesión
        public LoginResponse IniciarSesion(LoginRequest loginRequest)
        {
            if (!YaRegistrado(loginRequest.Email))
            {
                return new LoginResponse
                {
                    Exito = false,
                    Mensaje = "El correo no está registrado"
                };
            }

            Argon2Encryptation encriptador = new Argon2Encryptation();
            string contrasenaEncriptada = encriptador.EncriptarContrasenaArgon2(loginRequest.Contrasena);

            UserRepositorio userRepo = new UserRepositorio();
            bool inicioSesionExitoso = userRepo.IniciarSesion(loginRequest.Email, contrasenaEncriptada);

            if (inicioSesionExitoso)
            {
                UserDto usuario = new UserDto
                {
                    Id = userRepo.ObtenerIdUsuario(loginRequest.Email, contrasenaEncriptada),
                    idRol = userRepo.ObtenerIdRolUsuario(loginRequest.Email, contrasenaEncriptada),
                    Email = loginRequest.Email
                };

                return new LoginResponse
                {
                    Exito = true,
                    Mensaje = "Inicio de sesión exitoso",
                    Usuario = usuario
                };
            }
            else
            {
                return new LoginResponse
                {
                    Exito = false,
                    Mensaje = "Credenciales incorrectas"
                };
            }
        }
    }
}
