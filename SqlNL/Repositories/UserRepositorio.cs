using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.DynamicData;
using UdApp.Dtos;
using UdApp.Services;

namespace UdApp.Repositories
{
    public class UserRepositorio
    {
        private DatabaseConnection dbConnection;

        public UserRepositorio()
        {
            dbConnection = new DatabaseConnection();
        }

        public int Registro(UserDto userNuevo, bool esInsercion = true)
        {
            try
            {
                // Consulta de inserción utilizando parámetros parametrizados
                string consulta = "INSERT INTO [dbo].[USUARIO] (cedulaUsuario, nombreUsuario, apellidoUsuario, telefono, direccion, email, contrasena) " +
                                  "VALUES (@Cedula, @Nombre, @Apellido, @Telefono, @Direccion, @Email, @Contrasena)";

                // Crear comando SQL con parámetros
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@Cedula", userNuevo.Cedula);
                command.Parameters.AddWithValue("@Nombre", userNuevo.Nombre);
                command.Parameters.AddWithValue("@Apellido", userNuevo.Apellido);
                command.Parameters.AddWithValue("@Telefono", userNuevo.Telefono);
                command.Parameters.AddWithValue("@Direccion", userNuevo.Direccion);
                command.Parameters.AddWithValue("@Email", userNuevo.Email);
                command.Parameters.AddWithValue("@Contrasena", userNuevo.Contrasena);

                // Abrir conexión, ejecutar comando y cerrar conexión
                dbConnection.AbrirConexion();
                int rowsAffected = command.ExecuteNonQuery();
                dbConnection.CerrarConexion();

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
                dbConnection.AbrirConexion();

                string consulta = "SELECT idUsuario FROM USUARIO WHERE email = @Email AND contrasena = @Contrasena";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
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
                dbConnection.CerrarConexion();
            }
        }
        public int ObtenerIdRolUsuario(string email, string contrasena)
        {
            int idRolUsuario = 0;
            try
            {
                dbConnection.AbrirConexion();
                string consulta = "SELECT fk_idRol FROM USUARIO WHERE email = @Email AND contrasena = @Contrasena";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contrasena", contrasena);

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    idRolUsuario = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el idRolUsuario: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
            return idRolUsuario;
        }

        public bool IniciarSesion(string email, string contrasena)
        {
            try
            {
                string consulta = "SELECT COUNT(*) FROM [dbo].[USUARIO] WHERE email = @Email AND contrasena = @Contrasena";

                dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Contrasena", contrasena);
                int resultado = Convert.ToInt32(command.ExecuteScalar());
                dbConnection.CerrarConexion();

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

                dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@Email", correo);
                int resultado = Convert.ToInt32(command.ExecuteScalar());
                dbConnection.CerrarConexion();

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

                dbConnection.AbrirConexion();
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@Nombre", usuarioActualizado.Nombre);
                command.Parameters.AddWithValue("@Apellido", usuarioActualizado.Apellido);
                command.Parameters.AddWithValue("@Telefono", usuarioActualizado.Telefono);
                command.Parameters.AddWithValue("@Direccion", usuarioActualizado.Direccion);
                command.Parameters.AddWithValue("@Email", usuarioActualizado.Email);
                command.Parameters.AddWithValue("@Contrasena", usuarioActualizado.Contrasena);
                command.Parameters.AddWithValue("@Cedula", usuarioActualizado.Cedula);
                int rowsAffected = command.ExecuteNonQuery();
                dbConnection.CerrarConexion();

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
                dbConnection.AbrirConexion();

                string query = "SELECT idUsuario, nombreUsuario, apellidoUsuario, email, fk_idRol " +
                               "FROM USUARIO";
                SqlCommand command = new SqlCommand(query, dbConnection.Connection);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserDto usuario = new UserDto
                    {
                        Id = Convert.ToInt32(reader["idUsuario"]),
                        Nombre = reader["nombreUsuario"].ToString(),
                        Apellido = reader["apellidoUsuario"].ToString(),
                        Email = reader["email"].ToString(),
                        idRol = Convert.ToInt32(reader["fk_idRol"]),
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
                dbConnection.CerrarConexion();
            }
            return usuarios;
        }
        public List<RolDto> ObtenerRoles()
        {
            List<RolDto> roles = new List<RolDto>();
            try
            {
                dbConnection.AbrirConexion();

                string queryRoles = "SELECT idRol, nombreRol, permisos FROM Rol";
                SqlCommand commandRoles = new SqlCommand(queryRoles, dbConnection.Connection);

                SqlDataReader readerRoles = commandRoles.ExecuteReader();
                while (readerRoles.Read())
                {
                    RolDto rol = new RolDto
                    {
                        Id = Convert.ToInt32(readerRoles["idRol"]),
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
                dbConnection.CerrarConexion();
            }
            return roles;
        }
        public void GuardarRol(int idUsuario, int idRolSeleccionado)
        {
            try
            {
                dbConnection.AbrirConexion();

                string query = "UPDATE USUARIO SET fk_idRol = @IdRol WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(query, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdRol", idRolSeleccionado);
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
                dbConnection.CerrarConexion();
            }
        }
    }
}
