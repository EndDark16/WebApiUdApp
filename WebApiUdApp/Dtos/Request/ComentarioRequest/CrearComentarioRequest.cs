namespace WebApiUdApp.Dtos.Request.ComentarioRequest
{
    public class CrearComentarioRequest
    {
        public string Comentario { get; set; } = string.Empty;
        public int IdPublicacion { get; set; }
        
    }
}
