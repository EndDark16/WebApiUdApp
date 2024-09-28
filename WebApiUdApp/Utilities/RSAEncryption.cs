using System;
using System.Security.Cryptography;
using System.Text;

namespace WebApiUdApp.Utilities
{
    public class RSAEncryption
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public RSAEncryption()
        {
            // Crear una instancia de RSACryptoServiceProvider para generar el par de claves
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Obtener la clave pública y privada
                _publicKey = rsa.ExportParameters(false); // Clave pública
                _privateKey = rsa.ExportParameters(true); // Clave privada
            }
        }

        public string Encriptar(string texto)
        {
            // Crear una instancia de RSACryptoServiceProvider para encriptar
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Asignar la clave pública al proveedor de criptografía
                rsa.ImportParameters(_publicKey);

                // Convertir el texto a bytes
                byte[] bytesTexto = Encoding.UTF8.GetBytes(texto);

                // Encriptar los bytes
                byte[] bytesEncriptados = rsa.Encrypt(bytesTexto, false);

                // Convertir los bytes encriptados a una cadena base64 para almacenarla en la base de datos
                return Convert.ToBase64String(bytesEncriptados);
            }
        }

        public string Desencriptar(string textoEncriptado)
        {
            // Crear una instancia de RSACryptoServiceProvider para desencriptar
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                // Asignar la clave privada al proveedor de criptografía
                rsa.ImportParameters(_privateKey);

                // Convertir la cadena base64 encriptada a bytes
                byte[] bytesEncriptados = Convert.FromBase64String(textoEncriptado);

                // Desencriptar los bytes
                byte[] bytesDesencriptados = rsa.Decrypt(bytesEncriptados, false);

                // Convertir los bytes desencriptados a texto
                return Encoding.UTF8.GetString(bytesDesencriptados);
            }
        }
    }

}
