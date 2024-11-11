using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using WebApiUdApp.Dtos;
using WebApiUdApp.Services;
using WebApiUdApp.Utilities;

namespace WebApiUdApp.Repositories
{
    public class ComentarioRepositorio
    {
        private DatabaseConnection _dbConnection;

        public ComentarioRepositorio()
        {
            _dbConnection = new DatabaseConnection();
        }
        public List<ComentarioDto> GetComentariosByPublicacionId(int idPublicacion)
        {
            List<ComentarioDto> comentarios = new List<ComentarioDto>();

            try
            {
                _dbConnection.AbrirConexion();

                string consulta = @"SELECT C.idComentario, C.contenido, C.fechaComentario, C.fk_idUsuarioComentador, C.fk_idPublicacion,
                                    U.nombreUsuario +' ' + U.apellidoUsuario AS nombreUsuarioComentador
                                    FROM COMENTARIO C
                                    INNER JOIN USUARIO U ON C.fk_idUsuarioComentador = U.idUsuario
                                    WHERE fk_idPublicacion = @IdPublicacion";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ComentarioDto comentario = new ComentarioDto
                    {
                        IdComentario = reader.GetInt32("idComentario"),
                        Contenido = reader.GetString("contenido"),
                        FechaCreacion = reader.GetDateTime("fechaComentario"),
                        IdUsuario = reader.GetInt32("fk_idUsuarioComentador"),
                        IdPublicacion = reader.GetInt32("fk_idPublicacion"),
                        NombreUsuarioComentador = reader.GetString("nombreUsuarioComentador")
                    };

                    comentarios.Add(comentario);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentarios: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }

            return comentarios;
        }


        public ComentarioDto? GetUltimoComentarioRealizado(int idUsuario, int idPublicacion)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"SELECT TOP (1) idComentario, contenido, fechaComentario, fk_idUsuarioComentador, fk_idPublicacion
								  FROM COMENTARIO WHERE fk_idPublicacion = @IdPublicacion AND fk_idUsuarioComentador = @IdUsuario
								  ORDER BY fechaComentario DESC";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                command.Parameters.AddWithValue("@IdPublicacion", idPublicacion);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new ComentarioDto
                    {
                        IdComentario = reader.GetInt32("idComentario"),
                        Contenido = reader.GetString("contenido"),
                        IdUsuario = reader.GetInt32("fk_idUsuarioComentador"),
                        FechaCreacion = reader.GetDateTime("fechaComentario")
                    };
                }

                return null;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public ComentarioDto? GetComentarioById(int idComentario)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "SELECT * FROM COMENTARIO WHERE idComentario = @IdComentario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdComentario", idComentario);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new ComentarioDto
                    {
                        IdComentario = reader.GetInt32("idComentario"),
                        Contenido = reader.GetString("contenido"),
                        IdUsuario = reader.GetInt32("fk_idUsuarioComentador"),
                        FechaCreacion = reader.GetDateTime("fechaComentario")
                    };
                }

                return null;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public bool UpdateComentario(ComentarioDto comentarioDto)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"UPDATE COMENTARIO SET 
                                    contenido = @Contenido,
                                    fechaComentario = @FechaEdicion
                                    WHERE idComentario = @IdComentario AND fk_idPublicacion = @fk_idPublicacion AND fk_idUsuarioComentador = @fk_idUsuario";

                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Contenido", comentarioDto.Contenido);
                command.Parameters.AddWithValue("@FechaEdicion", comentarioDto.FechaCreacion);
                command.Parameters.AddWithValue("@IdComentario", comentarioDto.IdComentario);
                command.Parameters.AddWithValue("@fk_idPublicacion", comentarioDto.IdPublicacion);
                command.Parameters.AddWithValue("@fk_idUsuario", comentarioDto.IdUsuario);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al actualizar el comentario: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public bool DeleteComentario(int idComentario)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "DELETE FROM COMENTARIO WHERE idComentario = @IdComentario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdComentario", idComentario);

                return command.ExecuteNonQuery() > 0;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }

        public bool AddComentario(ComentarioDto comentarioNuevo)
        {

            try
            {
                string consulta = @"INSERT INTO COMENTARIO (contenido, fechaComentario, fk_idUsuarioComentador, fk_idPublicacion)
                                  VALUES (@Contenido, @FechaCreacion, @IdUsuario, @fk_idPublicacion)";

                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@Contenido", comentarioNuevo.Contenido);
                command.Parameters.AddWithValue("@FechaCreacion", comentarioNuevo.FechaCreacion);
                command.Parameters.AddWithValue("@IdUsuario", comentarioNuevo.IdUsuario);
                command.Parameters.AddWithValue("@fk_idPublicacion", comentarioNuevo.IdPublicacion);

                _dbConnection.AbrirConexion();
                int rowsAffected = command.ExecuteNonQuery();
                _dbConnection.CerrarConexion();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar comentario: " + ex.Message);
                return false;
            }
        }

        public List<ComentarioDto> ObtenerComentariosPorUsuario(int idUsuario)
        {
            List<ComentarioDto> comentarios = new List<ComentarioDto>();
            try
            {
                _dbConnection.AbrirConexion();

                string query = "SELECT * FROM COMENTARIO WHERE fk_idUsuarioComentador = @IdUsuario";
                SqlCommand command = new SqlCommand(query, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ComentarioDto comentario = new ComentarioDto
                    {
                        IdComentario = reader.GetInt32("idComentario"),
                        Contenido = reader.GetString("contenido"),
                        IdUsuario = reader.GetInt32("fk_idUsuarioComentador"),
                        FechaCreacion = reader.GetDateTime("fechaComentario")
                    };
                    comentarios.Add(comentario);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentarios por usuario: " + ex.Message);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
            return comentarios;
        }
    }
}
