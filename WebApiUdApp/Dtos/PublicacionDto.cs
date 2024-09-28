namespace WebApiUdApp.Dtos
{
    public class PublicacionDto
    {
        public int IdPublicacion { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public DateTime FechaPublicacion { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int NumeroComentarios { get; set; }
        public int NumeroLikes { get; set; }
        public bool Reportada { get; set; }
        public string MotivoReporte { get; set; } = string.Empty;
        public bool Like { get; set; } = false;
    }   
}
