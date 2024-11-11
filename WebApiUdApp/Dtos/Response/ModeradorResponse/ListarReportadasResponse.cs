namespace WebApiUdApp.Dtos.Response.ModeradorResponse
{
    public class ListarReportadasResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<ReporteDto> ReportesDto { get; set; } = new List<ReporteDto>();
    }
}
