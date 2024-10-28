namespace WebApiUdApp.Dtos.Request.PublicacionesRequest
{
    public class ActualizarPublicacionRequest
    {
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
    }
}
