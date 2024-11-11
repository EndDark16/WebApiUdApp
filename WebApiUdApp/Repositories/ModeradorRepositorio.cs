using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using WebApiUdApp.Dtos;
using WebApiUdApp.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApiUdApp.Repositories
{
    public class ModeradorRepositorio
    {
        private DatabaseConnection _dbConnection;

        public ModeradorRepositorio()
        {
            _dbConnection = new DatabaseConnection();
        }
        public List<ReporteDto> ObtenerPublicacionesReportadasMod()
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"SELECT R.idReporte,P.idPublicacion, P.titulo, P.fechaPublicacion,
                            UP.nombreUsuario + ' ' + UP.apellidoUsuario nombreUsuarioPublicador,
							R.motivoReporte, R.fechaReporte, R.fk_idUsuarioReportador idUsuarioReportador, 
							U.nombreUsuario + ' ' + U.apellidoUsuario nombreUsuarioReportador
                    FROM REPORTE R
					INNER JOIN PUBLICACION P ON R.fk_idPublicacionReportada = P.idPublicacion
					INNER JOIN USUARIO UP ON P.fk_idUsuarioPublicador = UP.idUsuario
                    INNER JOIN USUARIO U ON R.fk_idUsuarioReportador = U.idUsuario
                    ORDER BY R.fechaReporte DESC";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);

                SqlDataReader reader = command.ExecuteReader();

                List<ReporteDto> reportes = new List<ReporteDto>();

                while (reader.Read())
                {
                    ReporteDto reporte = new ReporteDto
                    {
                        IdReporte = reader.GetInt32("idReporte"),
                        IdPublicacionReportada = reader.GetInt32("idPublicacion"),
                        TituloPublicacion = reader.GetString("titulo"),
                        FechaPublicacion = reader.GetDateTime("fechaPublicacion"),
                        NombreUsuarioPublicacador = reader.GetString("nombreUsuarioPublicador"),
                        MotivoReporte = reader.GetString("motivoReporte"),
                        FechaReporte = reader.GetDateTime("fechaReporte"),
                        IdUsuarioReportador = reader.GetInt32("idUsuarioReportador"),
                        NombreUsuarioReportador = reader.GetString("nombreUsuarioReportador")

                    };
                    reportes.Add(reporte);
                }
                reader.Close();
                return reportes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener las publicaciones reportadas: " + ex.Message);
                return new List<ReporteDto>();
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
        public bool EliminarReporteMod(int idReporte)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = "DELETE FROM REPORTE WHERE idReporte = @IdReporte";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdReporte", idReporte);

                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
        public bool EliminarPublicacionMod(int idReporte)
        {
            try
            {
                _dbConnection.AbrirConexion();

                SqlCommand command = new SqlCommand("DeleteReporteAndRelatedPublicacion", _dbConnection.Connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Agrega el parámetro de entrada para el IdReporte
                command.Parameters.AddWithValue("@IdReporte", idReporte);

                // Configura el parámetro de retorno
                var returnParameter = command.Parameters.Add("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                // Ejecuta el comando
                command.ExecuteNonQuery();

                // Obtiene el valor de retorno
                int result = (int)returnParameter.Value;
                return result == 1; // Devuelve true si la operación fue exitosa
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar la publicación y sus datos relacionados: " + ex.Message);
                return false;
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
    }
}
