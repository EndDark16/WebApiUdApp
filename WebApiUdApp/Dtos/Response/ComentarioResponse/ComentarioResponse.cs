namespace WebApiUdApp.Dtos.Response.ComentarioResponse
{
    public class ComentarioResponse
    {
        public int IdComentario { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaComentario { get; set; }
        public string NombreCompletoUsuario { get; set; } = string.Empty;
    }
}
