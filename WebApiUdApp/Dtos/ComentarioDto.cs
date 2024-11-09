namespace WebApiUdApp.Dtos
{
    public class ComentarioDto
    {
        public int IdComentario { get; set; }
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }
        public string NombreUsuarioComentador { get; set; } = string.Empty;
    }
}
