namespace WebApiUdApp.Dtos.Request.PublicacionesRequest
{
    public class ActualizarPublicacionRequest
    {
        public int IdPublicacion { get; set; }
        public string Titulo { get; set; } = string.Empty;
    }
}
