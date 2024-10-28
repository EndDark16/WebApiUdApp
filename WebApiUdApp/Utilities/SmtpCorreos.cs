using System.Net;
using System.Net.Mail;

namespace WebApiUdApp.Utilities
{
    public class SmtpCorreos
    {
        public async Task EnviarCorreoConEstilo(string destinatario, string asunto, String bodyHtml)
        {
            try
            {
                // Configurar el cliente SMTP para enviar correos
                SmtpClient clienteSmtp = new SmtpClient("smtp.gmail.com"); // Reemplaza "smtp.servidor.com" por el servidor SMTP que corresponda
                clienteSmtp.Port = 587; // Puerto SMTP seguro (SSL/TLS)
                clienteSmtp.UseDefaultCredentials = false;
                clienteSmtp.Credentials = new NetworkCredential("udappx@gmail.com", "mnsrsyclbpiodmaj");
                clienteSmtp.EnableSsl = true;

                // Crear el mensaje de correo con HTML y estilos CSS en línea
                MailMessage mensaje = new MailMessage();
                mensaje.From = new MailAddress("udappx@gmail.com"); // Dirección de correo del remitente
                mensaje.To.Add(destinatario); // Agregar el destinatario0.
                mensaje.Subject = asunto; // Asunto del correo

                // Agregar el cuerpo del correo con estilos CSS en línea
                mensaje.IsBodyHtml = true;
                mensaje.Body = bodyHtml;
                // Enviar el correo
                await clienteSmtp.SendMailAsync(mensaje);
            }
            catch (Exception ex)
            {
                // Capturar errores en el envío de correo
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }
    }
}
