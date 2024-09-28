namespace WebApiUdApp.Dtos.Request.PublicacionesRequest
{
    public class ToggleLikeRequest
    {
        public int IdUsuario { get; set; }
        public int IdPublicacion { get; set; }
        public bool LikeStatus { get; set; }
    }
}
