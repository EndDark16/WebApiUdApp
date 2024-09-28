using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlClient;

namespace WebApiUdApp.Utilities
{
    public class ProteccionSQLInjection
    {
        private string connectionString; // Cadena de conexión a la base de datos

        public ProteccionSQLInjection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool EsSeguro(string consulta)
        {
            // Lógica para verificar la seguridad de la consulta
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(consulta, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        return true; // La consulta se ejecutó correctamente, consideramos segura
                    }
                    catch (SqlException)
                    {
                        return false; // La consulta falló, consideramos insegura
                    }
                }
            }
        }

        public bool EsSeguroValor(string valor)
        {
            // Lógica para verificar la seguridad del valor
            // Aquí podrías implementar validaciones específicas según el tipo de dato esperado
            return !string.IsNullOrWhiteSpace(valor); // Ejemplo básico: verifica que el valor no esté vacío
        }
    }
}
