namespace WebApiUdApp.Dtos
{
    public class UsuarioDto
    {
        public string Cedula { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool EstadoSuspension { get; set; }

    }
}

