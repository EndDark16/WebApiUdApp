using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UdApp.Dtos;

namespace UdApp.Services
{
    public class PublicacionesRepositorio : IDisposable
    {
        private DatabaseConnection dbConnection;

        public PublicacionesRepositorio()
        {
            dbConnection = new DatabaseConnection();
        }

        public List<PublicacionDto> PublicacionesRecientes(int idUsuario)
        {
            try
            {
                dbConnection.AbrirConexion();
                string consulta = @"SELECT P.idPublicacion, P.titulo, ISNULL(P.contenido, '') AS contenido, P.fechaPublicacion, 
                           U.nombreUsuario AS nombreUsuarioPublicador,
                           ISNULL(P.comentarios, 0) AS comentarios, ISNULL(P.likes, 0) AS likes
                    FROM PUBLICACION P
                    INNER JOIN USUARIO U ON P.fk_idUsuarioPublicador = U.idUsuario
                    ORDER BY P.fechaPublicacion DESC";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);

                // Utilizar parámetros parametrizados para prevenir SQL Injection
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

                    // Verificar y guardar el estado del "like"
                    publicacion.Like = UsuarioDioLike(idUsuario, publicacion.IdPublicacion);

                    publicaciones.Add(publicacion);
                }

                // Cerrar el lector después de haber leído todos los datos necesarios
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
                //dbConnection.AbrirConexion();

                // Consulta SQL para verificar si el usuario dio like a la publicación
                string consulta = "SELECT COUNT(*) FROM Likes WHERE fk_idUsuario = @IdUsuario AND fk_idPublicacion = @IdPublicacion";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;  // Si count es mayor que 0, significa que el usuario dio like
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al verificar si el usuario dio like: " + ex.Message);
                return false;
            }
            //finally
            //{
            //    dbConnection.CerrarConexion();
            //}
        }

        //public void InsertarPublicacion(string titulo, string contenido, DateTime fechaPublicacion, int idUsuarioPublicador, int comentarios, int likes)
        //{
        //    try
        //    {
        //        dbConnection.AbrirConexion();

        //        // Consulta SQL para insertar una nueva publicación
        //        string consulta = @"INSERT INTO PUBLICACION (titulo, contenido, fechaPublicacion, fk_idUsuarioPublicador, comentarios, likes)
        //                    VALUES (@Titulo, @Contenido, @FechaPublicacion, @IdUsuarioPublicador, @Comentarios, @Likes)";

        //        SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
        //        // Utilizar parámetros parametrizados para prevenir SQL Injection
        //        command.Parameters.AddWithValue("@Titulo", titulo);
        //        command.Parameters.AddWithValue("@Contenido", contenido);
        //        command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
        //        command.Parameters.AddWithValue("@IdUsuarioPublicador", idUsuarioPublicador);
        //        command.Parameters.AddWithValue("@Comentarios", comentarios);
        //        command.Parameters.AddWithValue("@Likes", likes);

        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar el error de alguna manera
        //        Console.WriteLine("Error al insertar la nueva publicación: " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbConnection.CerrarConexion();
        //    }
        //}
        public void InsertarPublicacion(string titulo, DateTime fechaPublicacion, int idUsuarioPublicador)
        {
            try
            {
                dbConnection.AbrirConexion();

                // Consulta SQL para insertar una nueva publicación sin comentarios ni likes inicialmente
                string consulta = @"INSERT INTO PUBLICACION (titulo, fechaPublicacion, fk_idUsuarioPublicador, comentarios, likes)
                            VALUES (@Titulo, @FechaPublicacion, @IdUsuarioPublicador, 0, 0)";

                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                // Utilizar parámetros parametrizados para prevenir SQL Injection
                command.Parameters.AddWithValue("@Titulo", titulo);
                command.Parameters.AddWithValue("@FechaPublicacion", fechaPublicacion);
                command.Parameters.AddWithValue("@IdUsuarioPublicador", idUsuarioPublicador);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al insertar la nueva publicación: " + ex.Message);
            }
            finally
            {
                dbConnection.CerrarConexion();
            }
        }
        public void ReportarPublicacion(int idPublicacion, string motivo)
        {
            try
            {
                dbConnection.AbrirConexion();
                // Consulta SQL para actualizar la publicación con el estado de reportada y el motivo
                string consulta = @"UPDATE PUBLICACION SET Reportada = 1, MotivoReporte = @Motivo WHERE idPublicacion = @IdPublicacion";

                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                // Utilizar parámetros parametrizados para prevenir SQL Injection
                command.Parameters.AddWithValue("@Motivo", motivo);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

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
        public void EliminarPublicacion(int idPublicacion)
        {
            try
            {
                dbConnection.AbrirConexion();

                // Consulta SQL para eliminar la publicación con el id proporcionado
                string consulta = @"DELETE FROM PUBLICACION WHERE idPublicacion = @IdPublicacion";
                SqlCommand command = new SqlCommand(consulta, dbConnection.Connection);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Manejar el error de alguna manera
                Console.WriteLine("Error al eliminar la publicación: " + ex.Message);
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
                string consulta = "INSERT INTO Likes (fk_idUsuario, fk_idPublicacion) VALUES (@IdUsuario, @IdPublicacion)";
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

        public void Dispose()
        {
            dbConnection.Dispose();
        }
    }
}
