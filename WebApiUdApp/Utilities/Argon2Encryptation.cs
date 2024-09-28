using Konscious.Security.Cryptography;
using System.Text;

namespace WebApiUdApp.Utilities
{
    public class Argon2Encryptation
    {
        public string EncriptarContrasenaArgon2(string contrasena)
        {
            // Crear un objeto Argon2 y generar el hash de la contraseña
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(contrasena));
            argon2.Salt = new byte[16]; // Generar una sal aleatoria de 16 bytes
            argon2.DegreeOfParallelism = 4; // Grado de paralelismo (puede ajustarse según la capacidad de tu sistema)
            argon2.MemorySize = 65536; // Tamaño de memoria en kilobytes
            argon2.Iterations = 4; // Número de iteraciones

            byte[] hashBytes = argon2.GetBytes(32); // Tamaño del hash en bytes (256 bits)

            // Convertir el hash a una cadena base64 para almacenarla en la base de datos
            string hashString = Convert.ToBase64String(hashBytes);

            return hashString;
        }

        public bool VerificarContrasenaArgon2(string contrasena, string hashAlmacenado)
        {
            // Convertir el hash almacenado de cadena base64 a bytes
            byte[] hashBytes = Convert.FromBase64String(hashAlmacenado);

            // Crear un objeto Argon2 y verificar la contraseña
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(contrasena));
            argon2.Salt = new byte[16]; // Debes almacenar la sal utilizada durante el encriptado para verificarla correctamente
            argon2.DegreeOfParallelism = 4; // Debes usar los mismos parámetros que se usaron para encriptar
            argon2.MemorySize = 65536; // Debes usar los mismos parámetros que se usaron para encriptar
            argon2.Iterations = 4; // Debes usar los mismos parámetros que se usaron para encriptar

            // Crear un nuevo hash utilizando la contraseña proporcionada
            byte[] newHashBytes = argon2.GetBytes(32);

            // Verificar si los hashes coinciden
            bool verificado = HashesSonIguales(hashBytes, newHashBytes);

            return verificado;
        }

        private bool HashesSonIguales(byte[] hash1, byte[] hash2)
        {
            if (hash1 == null || hash2 == null || hash1.Length != hash2.Length)
                return false;

            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] != hash2[i])
                    return false;
            }

            return true;
        }
    }
}
