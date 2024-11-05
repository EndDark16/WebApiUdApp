namespace WebApiUdApp.Dtos.Request.ReporteRequest
{
    public class EnviarReporteRequest
    {
        public string Destinatario { get; set; } = string.Empty;
        public string NombreReporte { get; set; } = string.Empty;
    }
}
