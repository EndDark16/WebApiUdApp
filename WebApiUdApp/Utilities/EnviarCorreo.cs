namespace WebApiUdApp.Utilities
{
    public class EnviarCorreo
    {
        public void CorreoInicioSesion(string destinatario, string nombreUsuario)
        {
            try
            {
                //destinatario = "andresfelipe16200411@gmail.com"; // agregado para pruebas xd
                string asunto = "Bienvenido a UdApp";
                //string imagePath = "~/Content/Imagenes/UdApp_sign.png"; // Ruta relativa de la imagen
                //string imagenBase64 = ObtenerImagenBase64(imagePath); // Convertir la imagen a base64
                //Console.WriteLine(imagenBase64);
                string imageUrl = "https://i.ibb.co/Kr5WhCp/Ud-App-sign.png"; // URL directa de la imagen
                string body = $@"
                <!DOCTYPE html>
                <html lang='en' xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:v='urn:schemas-microsoft-com:vml'>
                <head>
                    <title></title>
                    <meta content='text/html; charset=utf-8' http-equiv='Content-Type'/>
                    <meta content='width=device-width, initial-scale=1.0' name='viewport'/>
                    <style>
                        /* Estilos CSS */
                        body {{
                            margin: 0;
                            padding: 0;
                            background-color: #ffffff;
                            -webkit-text-size-adjust: none;
                            text-size-adjust: none;
                            color: #000000; /* Color de texto */
                            font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
                        }}

                        a[x-apple-data-detectors] {{
                            color: inherit !important;
                            text-decoration: inherit !important;
                        }}

                        p {{
                            line-height: 1.6;
                            color: #203558;
                        }}

                        .button_block {{
                            text-align: center;
                        }}

                        .alignment {{
                            display: inline-block;
                        }}

                        .button_block a {{
                            text-decoration: none;
                            display: inline-block;
                            color: #ffffff;
                            background-color: #002027;
                            border-radius: 10px;
                            padding: 8px 20px;
                            font-size: 16px;
                            text-align: center;
                        }}

                        h1 {{
                            color: #002027; /* Color del título */
                            margin-bottom: 20px;
                            text-align: center;
                        }}

                        .footer {{
                            color: #3a3a3a;
                            font-size: 14px;
                            line-height: 1.6;
                            text-align: center;
                            padding: 10px;
                        }}

                        /* Estilos para la imagen */
                        .image-container {{
                            text-align: center;
                        }}

                        .image-container img {{
                            display: inline-block;
                            max-width: 100%;
                            height: auto;
                        }}
                    </style>
                </head>
                <body>
                    <table border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'>
                        <tr>
                            <td>
                                <table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='100%'>
                                    <tr>
                                        <td>
                                            <table align='center' border='0' cellpadding='0' cellspacing='0' role='presentation' width='680'>
                                                <tr>
                                                    <td>
                                                        <div class='image-container'>
                                                           <img align='center' alt='company Logo' class='icon' height='auto' src='{imageUrl}' style='display: block; height: auto; margin: 0 auto; border: 0;' width='117'/>
                                                        </div>
                                                        <h1>Bienvenido a UdApp</h1>
                                                        <p>Hola {nombreUsuario}, te has registrado satisfactoriamente en UdApp. Gracias por elegirnos, esperamos que disfrutes de todo lo que nuestra plataforma tiene para ofrecerte.</p>
                                                        <div class='button_block'>
                                                            <div class='alignment'>
                                                                <a href='http://localhost:777/' style='text-decoration:none;' target='_blank'>Ir a UdApp</a>
                                                            </div>
                                                        </div>
                                                        <div class='footer'>
                                                            <em><strong>Nota: Este correo se genera automáticamente, por favor no lo responda.</strong></em>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                ";

                // Instancia de la clase CorreoElectronico
                SmtpCorreos correo = new SmtpCorreos();

                // Llamar al método para enviar el correo con estilo
                correo.EnviarCorreoConEstilo(destinatario, asunto, body);
            }
            catch (Exception ex)
            {
                // Capturar errores en el envío de correo
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }
        }

        /*private string ObtenerImagenBase64(string imagePath)
        {
            try
            {
                string absolutePath = HttpContext.Current.Server.MapPath(imagePath);
                byte[] imageBytes = System.IO.File.ReadAllBytes(absolutePath);
                string base64String = Convert.ToBase64String(imageBytes);
                Console.WriteLine(base64String);
                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener la imagen en base64: " + ex.Message);
                return string.Empty;
            }
        }*/
    }
}
