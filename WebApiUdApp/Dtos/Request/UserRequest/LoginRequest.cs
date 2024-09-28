namespace WebApiUdApp.Dtos.Request.UserRequest
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}
