namespace WebApiUdApp.Dtos.Request.PublicacionesRequest
{
    public class ReportarPublicacionRequest
    {
        public int IdPublicacion { get; set; }
        public string Motivo { get; set; } = string.Empty;

    }
}
