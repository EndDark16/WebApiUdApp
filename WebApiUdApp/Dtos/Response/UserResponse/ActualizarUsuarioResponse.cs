namespace WebApiUdApp.Dtos.Response.UserResponse
{
    public class ActualizarUsuarioResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public UsuarioDto? Usuario { get; set; }
    }
}
