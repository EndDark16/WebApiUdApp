using System;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using WebApiUdApp.Dtos;
using System.Configuration;
using Microsoft.Data.SqlClient;
namespace WebApiUdApp.Services
{
    public class DatabaseConnection : IDisposable
    {
        //private string DataSource = "GINALVARADO"; //Conexion Gina
        private string DataSource = ("localhost\\MSSQLSERVER09"); //Conexion Andres
        private string InitialCatalog = "UdAppDB";
        private bool IntegratedSecurity = true;
        private SqlConnection connection;

        public DatabaseConnection()
        {
            string connectionString = $"Data Source={DataSource};Initial Catalog={InitialCatalog};Integrated Security={IntegratedSecurity}; Encrypt=True;Trust Server Certificate=True";
            connection = new SqlConnection(connectionString);
        }
        public SqlConnection Connection // Agregar esta propiedad
        {
            get { return connection; }
        }
        public int AbrirConexion()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexión establecida correctamente.");
                return 1; // Conexión establecida correctamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al abrir la conexión: " + ex.Message);
            }
        }

        public void RealizarConsulta(string consulta, bool esInsercion = false)
        {
            AbrirConexion();
            try
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.CommandType = CommandType.Text;

                if (esInsercion)
                {
                    int filasAfectadas = command.ExecuteNonQuery();
                    Console.WriteLine("Filas afectadas: " + filasAfectadas);
                }
                else
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Procesar resultados de la consulta
                        Console.WriteLine(reader.GetString(0)); // Suponiendo que la primera columna es de tipo string
                    }
                    reader.Close();
                }

                Console.WriteLine("Consulta realizada correctamente.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }


        public int CerrarConexion()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Conexión cerrada correctamente.");
                    return 1; // Conexión cerrada correctamente
                }
                else
                {
                    Console.WriteLine("La conexión ya está cerrada.");
                    return 0; // La conexión ya está cerrada
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar la conexión: " + ex.Message);
            }
        }
        public T RealizarConsultaScalar<T>(string consulta)
        {
            AbrirConexion();
            try
            {
                SqlCommand command = new SqlCommand(consulta, connection);
                command.CommandType = CommandType.Text;

                // Ejecutar la consulta y devolver el valor escalar
                object result = command.ExecuteScalar();
                return (T)Convert.ChangeType(result, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar la consulta: " + ex.Message);
            }
            finally
            {
                CerrarConexion();
            }
        }

        // Otros métodos de la clase DatabaseConnection...

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Dispose();
                connection = null;
            }
        }
    }
}