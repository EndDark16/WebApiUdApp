namespace WebApiUdApp.Dtos.Response.UserResponse
{
    public class LoginResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public UserDto Usuario { get; set; }
    }

}
