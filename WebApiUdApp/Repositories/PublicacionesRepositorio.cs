using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.PublicacionesRequest;
using WebApiUdApp.Services;

namespace WebApiUdApp.Services
{
    public class PublicacionesRepositorio : IDisposable
    {
        private DatabaseConnection dbConnection;

        public PublicacionesRepositorio()
        {
            dbConnection = new DatabaseConnection();
        }
        //CRUD PUBLICACIONES
        public void CrearPublicacion(int idUsuarioPublicador, string titulo, DateTime fechaPublicacion)
        {
            try
            {
                dbConnection.AbrirConexion();
                using (SqlCommand command = new SqlCommand("sp_CrearPublicacion", dbConnection.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Titulo", titulo);
                    command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
                    command.Parameters.AddWithValue("@IdUsuarioPublicador", idUsuarioPublicador);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear la publicación: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }


        public PublicacionDto ObtenerPublicacionPorId(int id)
        {
            try
            {
                dbConnection.AbrirConexion();
                using (SqlCommand command = new SqlCommand("sp_ObtenerPublicacionPorId", dbConnection.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new PublicacionDto
                        {
                            IdPublicacion = reader.GetInt32("idPublicacion"),
                            Titulo = reader.GetString("titulo"),
                            Contenido = reader["contenido"] as string,
                            FechaPublicacion = reader.GetDateTime("fechaPublicacion"),
                            IdUsuarioPublicador = reader.GetInt32("fk_idUsuarioPublicador"),
                            NumeroLikes = reader.GetInt32("likes"),
                            NumeroComentarios = reader.GetInt32("comentarios"),
                            Reportada = reader.GetBoolean("reportada")
                        };
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la publicación: " + ex.Message);
                return null;
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }
        public bool ActualizarPublicacion(int idUsuario, ActualizarPublicacionRequest request)
        {
            try
            {
                dbConnection.AbrirConexion();
                using (SqlCommand command = new SqlCommand("sp_ActualizarPublicacion", dbConnection.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdPublicacion", request.IdPublicacion);
                    command.Parameters.AddWithValue("@Titulo", request.Titulo);
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar la publicación: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public bool EliminarPublicacion(int idPublicacion, int idUsuario)
        {
            try
            {
                dbConnection.AbrirConexion();
                using (SqlCommand command = new SqlCommand("sp_EliminarPublicacion", dbConnection.Connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                    // Parámetro de salida para recibir el resultado
                    SqlParameter resultadoParam = new SqlParameter("@Resultado", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultadoParam);

                    command.ExecuteNonQuery();
                    return (bool)resultadoParam.Value; // Devuelve true si se eliminó exitosamente
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la publicación: " + ex.Message);
                return false;
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }


        public List<PublicacionDto> PublicacionesRecientes(int idUsuario)
        {
            try
            {
                dbConnection.AbrirConexion();

                string consulta = @"
                    SELECT P.idPublicacion, P.fk_idUsuarioPublicador, P.titulo, ISNULL(P.contenido, '') AS contenido, P.fechaPublicacion, 
                           U.nombreUsuario +' ' + U.apellidoUsuario AS nombreUsuarioPublicador, reportada,
                           ISNULL(P.comentarios, 0) AS comentarios, ISNULL(P.likes, 0) AS likes,
	   
                           CASE 
                               WHEN EXISTS (SELECT 1 FROM Likes L WHERE L.fk_idPublicacion = P.idPublicacion AND L.fk_idUsuario = @idUsuario)
                               THEN CAST(1 AS BIT)
                               ELSE CAST(0 AS BIT)
                           END AS [Like]
                    FROM PUBLICACION P
                    INNER JOIN USUARIO U ON P.fk_idUsuarioPublicador = U.idUsuario
                    ORDER BY P.fechaPublicacion DESC";

                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);

                // Añadir el parámetro para prevenir SQL Injection
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                SqlDataReader reader = command.ExecuteReader();

                List<PublicacionDto> publicaciones = new List<PublicacionDto>();

                while (reader.Read())
                {
                    PublicacionDto publicacion = new PublicacionDto
                    {
                        IdPublicacion = reader.GetInt32("idPublicacion"),
                        IdUsuarioPublicador = reader.GetInt32("fk_idUsuarioPublicador"),
                        Titulo = reader.GetString("titulo"),
                        Contenido = reader.IsDBNull("contenido") ? string.Empty : reader.GetString("contenido"),
                        FechaPublicacion = reader.GetDateTime("fechaPublicacion"),
                        NombreUsuario = reader.GetString("nombreUsuarioPublicador"),
                        Reportada = reader.GetBoolean("reportada"),
                        NumeroComentarios = reader.IsDBNull("comentarios") ? 0 : reader.GetInt32("comentarios"),
                        NumeroLikes = reader.IsDBNull("likes") ? 0 : reader.GetInt32("likes"),
                        Like = reader.GetBoolean("Like") // Mapeando el valor del "like"
                    };

                    publicaciones.Add(publicacion);
                }

                // Cerrar el lector después de haber leído todos los datos necesarios
                reader.Close();
                dbConnection.CerrarConexion();
                return publicaciones;
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al obtener las publicaciones recientes: " + ex.Message);
                return new List<PublicacionDto>();
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public bool UsuarioDioLike(int idUsuario, int idPublicacion)
        {
            try
            {
                // Asegúrate de que la conexión esté abierta antes de ejecutar el comando
                if (dbConnection.Connection.State == System.Data.ConnectionState.Closed)
                {
                    dbConnection.Connection.Open();
                }

                // Consulta SQL para verificar si el usuario dio like a la publicación
                string consulta = "SELECT COUNT(*) FROM Likes WHERE fk_idUsuario = @IdUsuario AND fk_idPublicacion = @IdPublicacion";

                using (SqlCommand command = new SqlCommand(consulta, dbConnection.Connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;  // Si count es mayor que 0, significa que el usuario dio like
                }
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al verificar si el usuario dio like: " + ex.Message);
                return false;
            }
            finally
            {
                // Asegurarse de cerrar la conexión si está abierta
                if (dbConnection.Connection.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Connection.Close();
                }
            }
        }
        public void ReportarPublicacion(int idPublicacion, DateTime fechaReporte, string motivo, int idUsuarioReportador)
        {
            try
            {
                dbConnection.AbrirConexion();
                // Consulta SQL para actualizar la publicación con el estado de reportada y el motivo
                string consulta = @"INSERT INTO REPORTE (motivoReporte, fechaReporte, fk_idUsuarioReportador, fk_idPublicacionReportada)
						            VALUES (@motivo, @fechaReporte, @idUsuarioReportador, @idPublicacion)";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                // Utilizar parámetros parametrizados para prevenir SQL Injection
                command.Parameters.AddWithValue("@motivo", motivo);
                command.Parameters.AddWithValue("@fechaReporte", fechaReporte);
                command.Parameters.AddWithValue("@idUsuarioReportador", idUsuarioReportador);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                command.ExecuteNonQuery();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al reportar la publicación: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public List<PublicacionDto> ObtenerPublicacionesReportadas()
        {
            try
            {
                dbConnection.AbrirConexion();
                string consulta = @"SELECT P.idPublicacion, P.titulo, ISNULL(P.contenido, '') AS contenido, P.fechaPublicacion, 
                           U.nombreUsuario AS nombreUsuarioPublicador,
                           ISNULL(P.comentarios, 0) AS comentarios, ISNULL(P.likes, 0) AS likes
                    FROM PUBLICACION P
                    INNER JOIN USUARIO U ON P.fk_idUsuarioPublicador = U.idUsuario
                    WHERE P.Reportada = 1
                    ORDER BY P.fechaPublicacion DESC";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);

                SqlDataReader reader = command.ExecuteReader();

                List<PublicacionDto> publicaciones = new List<PublicacionDto>();

                while (reader.Read())
                {
                    PublicacionDto publicacion = new PublicacionDto
                    {
                        IdPublicacion = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Contenido = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                        FechaPublicacion = reader.GetDateTime(3),
                        NombreUsuario = reader.GetString(4),
                        NumeroComentarios = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                        NumeroLikes = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
                    };
                    publicaciones.Add(publicacion);
                }
                reader.Close();
                return publicaciones;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las publicaciones reportadas: " + ex.Message);
                return new List<PublicacionDto>();
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public void EliminarReporte(int idPublicacion)
        {
            try
            {
                dbConnection.AbrirConexion();

                // Consulta SQL para eliminar el reporte de la publicación con el id proporcionado
                string consulta = @"UPDATE PUBLICACION SET Reportada = 0, MotivoReporte = NULL WHERE idPublicacion = @IdPublicacion";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al eliminar el reporte: " + ex.Message);


            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public void GuardarLike(int idUsuario, int idPublicacion)
        {
            try
            {
                dbConnection.AbrirConexion();
                // Consulta SQL para insertar un nuevo like en la tabla Likes
                string consulta = "INSERT INTO LIKES (fk_idUsuario, fk_idPublicacion) VALUES (@IdUsuario, @IdPublicacion)";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar el like: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }

        }

        public void EliminarLike(int idUsuario, int idPublicacion)
        {
            try
            {
                dbConnection.AbrirConexion();

                // Consulta SQL para eliminar el like de la tabla Likes
                string consulta = "DELETE FROM Likes WHERE fk_idUsuario = @IdUsuario AND fk_idPublicacion = @IdPublicacion";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el like: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public List<PublicacionDto> ObtenerPublicacionesOrdenadasPorLikes()
        {
            try
            {
                dbConnection.AbrirConexion();

                string consulta = @"
            SELECT TOP 20 p.idPublicacion, p.titulo, p.fechaPublicacion, p.likes
            FROM PUBLICACION p
            ORDER BY p.likes DESC";

                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                SqlDataReader reader = command.ExecuteReader();
                List<PublicacionDto> publicaciones = new List<PublicacionDto>();

                while (reader.Read())
                {
                    PublicacionDto publicacion = new PublicacionDto
                    {
                        IdPublicacion = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        FechaPublicacion = reader.GetDateTime(2),
                        NumeroLikes = reader.GetInt32(3)
                    };

                    publicaciones.Add(publicacion);
                }

                reader.Close();
                return publicaciones;
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al obtener las publicaciones ordenadas por likes: " + ex.Message);
                return new List<PublicacionDto>();
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }

        public void Dispose()
        {
            dbConnection.Dispose();
        }
    }
}
