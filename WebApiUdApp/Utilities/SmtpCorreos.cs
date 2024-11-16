using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace WebApiUdApp.Utilities
{
    public class SmtpCorreos
    {
        public async Task EnviarCorreoConEstilo(string destinatario, string asunto, string bodyHtml)
        {
            try
            {
                using (var clienteSmtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("udappx@gmail.com", "mnsrsyclbpiodmaj"),
                    EnableSsl = true,
                    Timeout = 20000 //tiempo de espera
                })
                using (var mensaje = new MailMessage
                {
                    From = new MailAddress("udappx@gmail.com"),
                    Subject = asunto,
                    Body = bodyHtml,
                    IsBodyHtml = true
                })
                {                    mensaje.To.Add(destinatario); // Agregar el destinatario
                    await clienteSmtp.SendMailAsync(mensaje);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }

        public async Task EnviarCorreoConPDFAdjunto(string destinatario, string asunto, string body, byte[] pdfBytes, string nombrepdf)
        {
            try
            {
                using (var clieneSmtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("udappx@gmail.com", "mnsrsyclbpiodmaj"),
                    EnableSsl = true,
                })
                using (var mailMessage = new MailMessage
                {
                    From = new MailAddress("udappx@gmail.com"),
                    Subject = asunto,
                    Body = body,
                    IsBodyHtml = true,
                })
                {
                    mailMessage.To.Add(destinatario);

                    // Crear el adjunto desde el PDF en bytes
                    using (var ms = new MemoryStream(pdfBytes))
                    {
                        var attachment = new Attachment(ms, $"{nombrepdf}.pdf", MediaTypeNames.Application.Pdf);
                        mailMessage.Attachments.Add(attachment);

                        await clieneSmtp.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }

    }
}
