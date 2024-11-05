namespace WebApiUdApp.Dtos
{
    public class ReporteDto
    {
        public int IdReporte { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
        public DateTime FechaReporte { get; set; }
        public int IdUsuarioReportador { get; set; }
        public int IdPublicacionReportada { get; set; }
        public string? NombreUsuarioReportador { get; set; } = string.Empty;
    }
}
