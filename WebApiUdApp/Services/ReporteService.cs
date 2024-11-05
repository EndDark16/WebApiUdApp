using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using WebApiUdApp.Utilities;
using WebApiUdApp.Repositories;

namespace WebApiUdApp.Services
{
    public class ReporteService
    {
        private readonly EnviarCorreoConPDF _enviarCorreoConPDF;

        public ReporteService(EnviarCorreoConPDF enviarCorreoConPDF)
        {
            _enviarCorreoConPDF = enviarCorreoConPDF;
        }

        public async Task EnviarCorreoReporteAsync(string token, string nombreReporte)
        {
            ReporteRepositorio reporteRepositorio = new ReporteRepositorio(); 
            int idUsuario = ObtenerIdUsuarioDesdeToken(token);
            string destinatario = reporteRepositorio.ObtenerCorreoPorId(idUsuario);
            // Llama al método CorreoReporte de EnviarCorreoConPDF
            await _enviarCorreoConPDF.CorreoReporte(destinatario, nombreReporte);

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
