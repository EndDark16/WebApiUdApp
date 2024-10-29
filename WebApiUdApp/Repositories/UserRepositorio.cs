using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApiUdApp.Dtos;
using WebApiUdApp.Services;
using WebApiUdApp.Utilities;

namespace WebApiUdApp.Repositories
{
    public class UserRepositorio
    {
        private DatabaseConnection _dbConnection;

        public UserRepositorio()
        {
            _dbConnection = new DatabaseConnection();
        }
        public UserDto? GetUsuarioById(int idUsuario)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "SELECT * FROM USUARIO WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new UserDto
                    {
                        IdUsuario = reader.GetInt32("idUsuario"),
                        Cedula = reader["cedulaUsuario"].ToString(),
                        Nombre = reader["nombreUsuario"].ToString(),
                        Apellido  = reader["apellidoUsuario"].ToString(),
                        Telefono = reader["telefono"].ToString(),
                        Direccion = reader["direccion"].ToString(),
                        Email = reader["email"].ToString(),
                        Contrasena = reader["contrasena"].ToString(),
                        IdRol = reader.IsDBNull("fk_IdRol") ? null : reader.GetInt32("fk_IdRol"),
                        EstadoSuspension = reader.IsDBNull("estadoSuspension") ? null : reader.GetBoolean("estadoSuspension")
                    };
                }

                return null;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public bool UpdateUsuario(int idUsuario, UserDto usuarioDto)
        {
            try
            {
                Argon2Encryptation encriptador = new Argon2Encryptation();
                usuarioDto.Contrasena = encriptador.EncriptarContrasenaArgon2(usuarioDto.Contrasena);
                _dbConnection.AbrirConexion();
                string consulta = @"UPDATE USUARIO SET 
                                cedulaUsuario = @CedulaUsuario,
                                nombreUsuario = @NombreUsuario,
                                apellidoUsuario = @ApellidoUsuario,
                                telefono = @Telefono,
                                direccion = @Direccion,
                                email = @Email,
                                contrasena = @Contrasena,
                                fk_IdRol = @IdRol,
                                estadoSuspension = @EstadoSuspension
                                WHERE idUsuario = @IdUsuario"
                ;

                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@CedulaUsuario", usuarioDto.Cedula);
                command.Parameters.AddWithValue("@NombreUsuario", usuarioDto.Nombre);
                command.Parameters.AddWithValue("@ApellidoUsuario", usuarioDto.Apellido);
                command.Parameters.AddWithValue("@Telefono", usuarioDto.Telefono);
                command.Parameters.AddWithValue("@Direccion", usuarioDto.Direccion);
                command.Parameters.AddWithValue("@Email", usuarioDto.Email);
                command.Parameters.AddWithValue("@Contrasena", usuarioDto.Contrasena);
                command.Parameters.AddWithValue("@IdRol", usuarioDto.IdRol ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@EstadoSuspension", usuarioDto.EstadoSuspension ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al actualizar el usuario: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public bool DeleteUsuario(int idUsuario)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "DELETE FROM USUARIO WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                return command.ExecuteNonQuery() > 0;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public int Registro(UserDto userNuevo, bool esInsercion = true)
        {
            try
            {
                // Consulta de inserción utilizando parámetros parametrizados
                string consulta = "INSERT INTO [dbo].[USUARIO] (cedulaUsuario, nombreUsuario, apellidoUsuario, telefono, direccion, email, contrasena) " +
                                  "VALUES (@Cedula, @Nombre, @Apellido, @Telefono, @Direccion, @Email, @Contrasena)";

                // Crear comando SQL con parámetros
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Cedula", userNuevo.Cedula);
                command.Parameters.AddWithValue("@Nombre", userNuevo.Nombre);
                command.Parameters.AddWithValue("@Apellido", userNuevo.Apellido);
                command.Parameters.AddWithValue("@Telefono", userNuevo.Telefono);
                command.Parameters.AddWithValue("@Direccion", userNuevo.Direccion);
                command.Parameters.AddWithValue("@Email", userNuevo.Email);
                command.Parameters.AddWithValue("@Contrasena", userNuevo.Contrasena);

                // Abrir conexión, ejecutar comando y cerrar conexión
                _dbConnection.AbrirConexion();
                int rowsAffected = command.ExecuteNonQuery();
                _dbConnection.CerrarConexion();

                // Verificar si la inserción fue exitosa
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Registro exitoso");
                    return 1;
                }
                else
                {
                    Console.WriteLine("Error al registrar usuario: No se insertaron filas");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar usuario: " + ex.Message);
                return 0;
            }
        }

        public int ObtenerIdUsuario(string email, string contrasena)
        {
            try
            {
                _dbConnection.AbrirConexion();

                string consulta = "SELECT idUsuario FROM USUARIO WHERE email = @Email AND contrasena = @Contrasena";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contrasena", contrasena);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el ID del usuario: " + ex.Message);
                return -1;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
        public int ObtenerIdRolUsuario(string email, string contrasena)
        {
            int IdRolUsuario = 0;
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "SELECT fk_IdRol FROM USUARIO WHERE email = @Email AND contrasena = @Contrasena";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contrasena", contrasena);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    IdRolUsuario = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el IdRolUsuario: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
            return IdRolUsuario;
        }

        public bool IniciarSesion(string email, string contrasena)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM [dbo].[USUARIO] WHERE email = @Email AND contrasena = @Contrasena";

                _dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contrasena", contrasena);
                int resultado = Convert.ToInt32(command.ExecuteScalar());
                _dbConnection.CerrarConexion();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al iniciar sesión: " + ex.Message);
                return false;
            }
        }

        public bool CorreoYaRegistrado(string correo)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM [dbo].[USUARIO] WHERE email = @Email";

                _dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", correo);
                int resultado = Convert.ToInt32(command.ExecuteScalar());
                _dbConnection.CerrarConexion();

                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al verificar correo: " + ex.Message);
                return false;
            }
        }

        public bool ActualizarDatosUsuario(UserDto usuarioActualizado)
        {
            try
            {
                string consulta = "UPDATE [dbo].[USUARIO] " +
                                  "SET nombreUsuario = @Nombre, " +
                                  "apellidoUsuario = @Apellido, " +
                                  "telefono = @Telefono, " +
                                  "direccion = @Direccion, " +
                                  "email = @Email, " +
                                  "contrasena = @Contrasena " +
                                  "WHERE cedulaUsuario = @Cedula";

                _dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Nombre", usuarioActualizado.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuarioActualizado.Apellido);
                command.Parameters.AddWithValue("@Telefono", usuarioActualizado.Telefono);
                command.Parameters.AddWithValue("@Direccion", usuarioActualizado.Direccion);
                command.Parameters.AddWithValue("@Email", usuarioActualizado.Email);
                command.Parameters.AddWithValue("@Contrasena", usuarioActualizado.Contrasena);
                command.Parameters.AddWithValue("@Cedula", usuarioActualizado.Cedula);
                int rowsAffected = command.ExecuteNonQuery();
                _dbConnection.CerrarConexion();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar datos del usuario: " + ex.Message);
                return false;
            }
        }


        /*      Funciones para el administrador     */

        public List<UserDto> ObtenerUsuarios()
        {
            List<UserDto> usuarios = new List<UserDto>();
            try
            {
                _dbConnection.AbrirConexion();

                string query = "SELECT idUsuario, nombreUsuario, apellidoUsuario, email, fk_IdRol " +
                               "FROM USUARIO";
                SqlCommand command = new SqlCommand(query, _dbConnection.Connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserDto usuario = new UserDto
                    {
                        IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                        Nombre = reader["nombreUsuario"].ToString(),
                        Apellido = reader["apellidoUsuario"].ToString(),
                        Email = reader["email"].ToString(),
                        IdRol = Convert.ToInt32(reader["fk_IdRol"]),
                    };
                    usuarios.Add(usuario);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener usuarios: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
            return usuarios;
        }
        public List<RolDto> ObtenerRoles()
        {
            List<RolDto> roles = new List<RolDto>();
            try
            {
                _dbConnection.AbrirConexion();

                string queryRoles = "SELECT IdRol, nombreRol, permisos FROM Rol";
                SqlCommand commandRoles = new SqlCommand(queryRoles, _dbConnection.Connection);

                SqlDataReader readerRoles = commandRoles.ExecuteReader();
                while (readerRoles.Read())
                {
                    RolDto rol = new RolDto
                    {
                        Id = Convert.ToInt32(readerRoles["IdRol"]),
                        Nombre = readerRoles["nombreRol"].ToString(),
                        Permisos = Convert.ToInt32(readerRoles["permisos"])
                    };
                    roles.Add(rol);
                }
                readerRoles.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los roles: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
            return roles;
        }
        public void GuardarRol(int idUsuario, int IdRolSeleccionado)
        {
            try
            {
                _dbConnection.AbrirConexion();

                string query = "UPDATE USUARIO SET fk_IdRol = @IdRol WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdRol", IdRolSeleccionado);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el rol: " + ex.Message);
                // Manejar el error según sea necesario
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
    }
}
