namespace WebApiUdApp.Dtos.Response.AdminResponse
{
    public class ObtenerUsuariosResponse
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<UsuarioDto> Usuarios { get; set; } = new List<UsuarioDto>();
    }
}
