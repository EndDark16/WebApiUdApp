namespace WebApiUdApp.Dtos.Request.PublicacionesRequest
{
    public class HacerPublicacionRequest
    {
        public int IdUsuario { get; set; }
        public string Titulo { get; set; } = string.Empty;
    }
}
