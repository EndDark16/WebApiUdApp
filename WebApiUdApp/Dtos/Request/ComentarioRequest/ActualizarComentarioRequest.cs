namespace WebApiUdApp.Dtos.Request.ComentarioRequest
{
    public class ActualizarComentarioRequest
    {
        public int IdComentario { get; set; }
        public int IdPublicacion { get; set; }
        public string Contenido { get; set; } = string.Empty;
        
    }
}
