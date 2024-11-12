using Microsoft.Data.SqlClient;
using WebApiUdApp.Dtos;
using WebApiUdApp.Services;

namespace WebApiUdApp.Repositories
{
    public class AdminRepositorio
    {
        private DatabaseConnection _dbConnection;

        public AdminRepositorio()
        {
            _dbConnection = new DatabaseConnection();
        }

        // Método para obtener la lista de usuarios con sus roles y estados de suspensión
        public List<UsuarioDto> ObtenerUsuarios()
        {
            List<UsuarioDto> usuarios = new List<UsuarioDto>();

            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"SELECT TOP (1000) U.idUsuario,
                                       U.nombreUsuario + ' ' + U.apellidoUsuario AS nombreUsuario,
                                       U.email,
                                       R.nombreRol,
                                       U.estadoSuspension
                                FROM USUARIO U
                                INNER JOIN ROL R ON R.idRol = U.fk_idRol";

                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioDto usuario = new UsuarioDto
                    {
                        IdUsuario = reader.GetInt32(reader.GetOrdinal("idUsuario")),
                        NombreUsuario = reader.GetString(reader.GetOrdinal("nombreUsuario")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        NombreRol = reader.GetString(reader.GetOrdinal("nombreRol")),
                        EstadoSuspension = reader.GetBoolean(reader.GetOrdinal("estadoSuspension"))
                    };
                    usuarios.Add(usuario);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener los usuarios: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }

            return usuarios;
        }

        // Método para cambiar el rol de un usuario
        public bool CambiarRolUsuario(int idUsuario, string nuevoRol)
        {
            try
            {
                _dbConnection.AbrirConexion();

                // Obtener el idRol del nuevoRol
                string consultaRol = @"SELECT idRol FROM ROL WHERE nombreRol = @nombreRol";
                SqlCommand commandRol = new SqlCommand(consultaRol, _dbConnection.Connection);
                commandRol.Parameters.AddWithValue("@nombreRol", nuevoRol);
                int nuevoIdRol = (int)commandRol.ExecuteScalar();

                // Actualizar el fk_idRol en la tabla USUARIO
                string consultaUpdate = @"UPDATE USUARIO SET fk_idRol = @idRol WHERE idUsuario = @idUsuario";
                SqlCommand commandUpdate = new SqlCommand(consultaUpdate, _dbConnection.Connection);
                commandUpdate.Parameters.AddWithValue("@idRol", nuevoIdRol);
                commandUpdate.Parameters.AddWithValue("@idUsuario", idUsuario);

                int rowsAffected = commandUpdate.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar el rol del usuario: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        // Método para cambiar el estado de suspensión de un usuario
        public bool CambiarEstadoSuspension(int idUsuario)
        {
            try
            {
                _dbConnection.AbrirConexion();

                // Cambia el estado de suspensión de 0 a 1 o de 1 a 0
                string consulta = @"UPDATE USUARIO
                                SET estadoSuspension = CASE WHEN estadoSuspension = 1 THEN 0 ELSE 1 END
                                WHERE idUsuario = @idUsuario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cambiar el estado de suspensión del usuario: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
    }
}
