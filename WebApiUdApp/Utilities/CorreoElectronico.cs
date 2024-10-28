using System.Net;
using System.Net.Mail;

namespace WebApiUdApp.Utilities
{
    public class CorreoElectronico
    {
        public void EnviarCorreoConEstilo(string destinatario, string asunto, string cuerpoHtml)
        {
            try
            {
                // Configurar el cliente SMTP para enviar correos
                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com "); // Reemplaza "smtp.servidor.com" por el servidor SMTP que corresponda
                clienteSmtp.Port = 587; // Puerto SMTP seguro (SSL/TLS)
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.Credentials = new NetworkCredential("udappx@gmail.com", "mnsrsyclbpiodmaj");
                clienteSmtp.EnableSsl = true;

                // Crear el mensaje de correo con HTML y estilos CSS en línea
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("udappx@gmail.com"); // Dirección de correo del remitente
                mensaje.To.Add(destinatario); // Agregar el destinatario
                mensaje.Subject = asunto; // Asunto del correo

                // Agregar el cuerpo del correo con estilos CSS en línea
                mensaje.IsBodyHtml = true;
                mensaje.Body = "<html><head><style>" +
                    "body { font-family: Arial, sans-serif; }" +
                    "h1 { color: #ff0000; }" +
                    ".parrafo { font-size: 16px; color: #333; }" +
                    "</style></head><body>" +
                    "<h1>Correo con Estilo</h1>" +
                    "<p class='parrafo'>" + cuerpoHtml + "</p>" +
                    "</body></html>";

                // Enviar el correo
                clienteSmtp.Send(mensaje);
            }
            catch (Exception ex)
            {
                // Capturar errores en el envío de correo
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }
    }
}
