namespace WebApiUdApp.Dtos.Response.ComentarioResponse
{
    public class ActualizarComentarioResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ComentarioDto? ComentarioActualizado { get; set; }
    }
}
