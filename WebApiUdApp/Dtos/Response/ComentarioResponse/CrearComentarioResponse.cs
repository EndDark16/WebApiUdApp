namespace WebApiUdApp.Dtos.Response.ComentarioResponse
{
    public class CrearComentarioResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public ComentarioDto? Comentario { get; set; }
    }
}
