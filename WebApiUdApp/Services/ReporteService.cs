using System.Threading.Tasks;
using WebApiUdApp.Utilities;

namespace WebApiUdApp.Services
{
    public class ReporteService
    {
        private readonly EnviarCorreoConPDF _enviarCorreoConPDF;

        public ReporteService(EnviarCorreoConPDF enviarCorreoConPDF)
        {
            _enviarCorreoConPDF = enviarCorreoConPDF;
        }

        public async Task EnviarCorreoReporteAsync(string destinatario, string nombreReporte)
        {
            // Llama al método CorreoReporte de EnviarCorreoConPDF
            await _enviarCorreoConPDF.CorreoReporte(destinatario, nombreReporte);
        }
    }
}
