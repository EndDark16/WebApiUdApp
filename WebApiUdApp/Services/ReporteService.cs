using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using WebApiUdApp.Utilities;
using WebApiUdApp.Repositories;
using WebApiUdApp.Dtos;
using DinkToPdf.Contracts;

namespace WebApiUdApp.Services
{
    public class ReporteService
    {
        private readonly IConverter _pdfConverter;
        private readonly SmtpCorreos _correo;
        private readonly ReporteRepositorio _reportesRepositorio;
        private readonly GenerarHtmlString _generarHtmlString;

        public ReporteService(GenerarHtmlString generarHtmlString, IConverter pdfConverter, SmtpCorreos correo, ReporteRepositorio reportesRepositorio)
        {
            _pdfConverter = pdfConverter;
            _correo = correo;
            _reportesRepositorio = reportesRepositorio;
            _generarHtmlString = generarHtmlString;
        }

        public async Task EnviarCorreoReporteAsync(string token)
        {
            ReporteRepositorio reporteRepositorio = new ReporteRepositorio(); 
            int idUsuario = ObtenerIdUsuarioDesdeToken(token);
            string destinatario = reporteRepositorio.ObtenerCorreoPorId(idUsuario);
            // Llama al método CorreoReporte de EnviarCorreoConPDF
            try
            {

                //destinatario = "andresfelipe16200411@gmail.com"; // Para pruebas
                string asunto = $"Reporte de Publicaciones Reportadas - UdApp";
                string body = "Adjunto se encuentra el reporte en formato PDF.";
                string nombrePDF = "PublicacionesReportadas";

                // Obtener los datos de reportes
                List<ReporteDto> reportesDto = _reportesRepositorio.ObtenerPublicacionesReportadas();

                // Generar el HTML para el PDF
                string htmlContent = _generarHtmlString.GenerateHtmlReporteReportadas(reportesDto);

                // Convertir HTML a PDF usando GenerarPDF
                GenerarPDF _generarPDF = new GenerarPDF(_pdfConverter);
                byte[] pdfBytes = _generarPDF.GeneratePdfFromHtml(htmlContent);

                // Enviar el correo con el PDF adjunto
                await _correo.EnviarCorreoConPDFAdjunto(destinatario, asunto, body, pdfBytes, nombrePDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }

        }
        public async Task EnviarCorreoPopularesAsync(string token)
        {
            try
            {
                ReporteRepositorio reporteRepositorio = new ReporteRepositorio();
                int idUsuario = ObtenerIdUsuarioDesdeToken(token);
                string destinatario = reporteRepositorio.ObtenerCorreoPorId(idUsuario);
                //destinatario = "andresfelipe16200411@gmail.com"; // Para pruebas
                string asunto = $"Reporte de publicaciones mas Likeadas - UdApp";
                string body = "Adjunto se encuentra el reporte en formato PDF.";
                string nombrePDF = "PublicacionesMasLikeadas";
                PublicacionesRepositorio _publicacionesRepositorio = new PublicacionesRepositorio();
                // Obtener los datos de reportes
                List<PublicacionDto> publicacionesDto = _publicacionesRepositorio.ObtenerPublicacionesOrdenadasPorLikes();

                // Generar el HTML para el PDF
                string htmlContent = _generarHtmlString.GenerateHtmlReporteLikeadas(publicacionesDto);

                // Convertir HTML a PDF usando GenerarPDF
                GenerarPDF _generarPDF = new GenerarPDF(_pdfConverter);
                byte[] pdfBytes = _generarPDF.GeneratePdfFromHtml(htmlContent);

                // Enviar el correo con el PDF adjunto
                await _correo.EnviarCorreoConPDFAdjunto(destinatario, asunto, body, pdfBytes, nombrePDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }

        }
        public async Task EnviarCorreoRegistrosAsync(string token)
        {
            ReporteRepositorio reporteRepositorio = new ReporteRepositorio();
            int idUsuario = ObtenerIdUsuarioDesdeToken(token);
            string destinatario = reporteRepositorio.ObtenerCorreoPorId(idUsuario);
            // Llama al método CorreoReporte de EnviarCorreoConPDF
            try
            {

                //destinatario = "andresfelipe16200411@gmail.com"; // Para pruebas
                string asunto = $"Reporte de Usuarios registrados en los ultimos 30 días - UdApp";
                string body = "Adjunto se encuentra el reporte en formato PDF.";
                string nombrePDF = "ReporteUsuarios";

                // Obtener los datos de reportes
                List<ReporteUsuariosDto> reporteUsuariosDto = _reportesRepositorio.ObtenerUsuariosRegistradosUltimos30Dias();

                // Generar el HTML para el PDF
                string htmlContent = _generarHtmlString.GenerateHtmlReporteUsuariosRegistradosConImagen(reporteUsuariosDto);

                // Convertir HTML a PDF usando GenerarPDF
                GenerarPDF _generarPDF = new GenerarPDF(_pdfConverter);
                byte[] pdfBytes = _generarPDF.GeneratePdfFromHtml(htmlContent);

                // Enviar el correo con el PDF adjunto
                await _correo.EnviarCorreoConPDFAdjunto(destinatario, asunto, body, pdfBytes, nombrePDF);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
            }

        }
        public byte[] GenerarReportePublicacionesReportadas()
        {
            // Obtener los datos de reportes
            List<ReporteDto> reportesDto = _reportesRepositorio.ObtenerPublicacionesReportadas();

            // Generar el HTML para el PDF
            string htmlContent = _generarHtmlString.GenerateHtmlReporteReportadas(reportesDto);

            // Convertir HTML a PDF
            GenerarPDF generadorPDF = new GenerarPDF(_pdfConverter);
            return generadorPDF.GeneratePdfFromHtml(htmlContent);
        }

        public byte[] GenerarReportePublicacionesPopulares()
        {
            PublicacionesRepositorio _publicacionesRepositorio = new PublicacionesRepositorio();
            // Obtener los datos de reportes
            List<PublicacionDto> publicacionesDto = _publicacionesRepositorio.ObtenerPublicacionesOrdenadasPorLikes();

            // Generar el HTML para el PDF
            string htmlContent = _generarHtmlString.GenerateHtmlReporteLikeadas(publicacionesDto);

            // Convertir HTML a PDF
            GenerarPDF generadorPDF = new GenerarPDF(_pdfConverter);
            return generadorPDF.GeneratePdfFromHtml(htmlContent);
        }
        public byte[] GenerarReporteUsuariosPdf(string token)
        {
            try
            {
                ReporteRepositorio reporteRepositorio = new ReporteRepositorio();
                int idUsuario = ObtenerIdUsuarioDesdeToken(token);

                // Obtener los datos de reportes
                List<ReporteUsuariosDto> reporteUsuariosDto = _reportesRepositorio.ObtenerUsuariosRegistradosUltimos30Dias();

                // Generar el HTML para el PDF
                string htmlContent = _generarHtmlString.GenerateHtmlReporteUsuariosRegistradosConImagen(reporteUsuariosDto);

                // Convertir HTML a PDF usando GenerarPDF
                GenerarPDF _generarPDF = new GenerarPDF(_pdfConverter);
                byte[] pdfBytes = _generarPDF.GeneratePdfFromHtml(htmlContent);

                return pdfBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al generar el PDF: " + ex.Message);
                return null;
            }
        }
        private int ObtenerIdUsuarioDesdeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            string sid = jsonToken?.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid)?.Value;

            if (string.IsNullOrEmpty(sid))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }

            return int.Parse(sid);
        }

    }
}
