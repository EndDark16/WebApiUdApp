using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using WebApiUdApp.Dtos;
using WebApiUdApp.Services;

namespace WebApiUdApp.Repositories
{
    public class ReporteRepositorio
    {
        private DatabaseConnection _dbConnection;

        public ReporteRepositorio()
        {
            _dbConnection = new DatabaseConnection();
        }
        public List<ReporteDto> ObtenerPublicacionesReportadas()
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"SELECT R.idReporte, R.motivoReporte, R.fechaReporte, R.fk_idUsuarioReportador, R.fk_idPublicacionReportada,
                           U.nombreUsuario + ' ' + U.apellidoUsuario nombreUsuarioReportador
                    FROM REPORTE R
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
                         MotivoReporte = reader.GetString("motivoReporte"),
                         FechaReporte = reader.GetDateTime("fechaReporte"),
                         IdUsuarioReportador = reader.GetInt32("fk_idUsuarioReportador"),
                         IdPublicacionReportada = reader.GetInt32("fk_idPublicacionReportada"),
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
        public string ObtenerCorreoPorId(int idUsuario)
        {
            try
            {
                _dbConnection.AbrirConexion();
                string consulta = @"SELECT [email]
                                  FROM USUARIO WHERE idUsuario = @IdUsuario";
                SqlCommand command = new SqlCommand(consulta, _dbConnection.Connection);
                command.Parameters.AddWithValue("@IdUsuario", idUsuario);

                // Ejecutar la consulta y obtener el resultado
                object resultado = command.ExecuteScalar();

                // Comprobar si el resultado no es null y devolverlo como string
                return resultado != null ? resultado.ToString() : null;
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes registrar o lanzar la excepción según tu necesidad)
                throw new Exception("Error al obtener el correo electrónico", ex);
            }
            finally
            {
                _dbConnection.CerrarConexion();
            }
        }
    }
}
