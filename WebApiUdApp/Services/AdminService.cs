using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Response;
using WebApiUdApp.Dtos.Response.AdminResponse;
using WebApiUdApp.Repositories;

namespace WebApiUdApp.Services
{
    public class AdminService
    {
        private readonly AdminRepositorio _adminRepositorio;

        public AdminService(AdminRepositorio adminRepositorio)
        {
            _adminRepositorio = adminRepositorio;
        }

        public ObtenerUsuariosResponse ObtenerUsuarios()
        {
            try
            {
                List<UsuarioDto> usuarios = _adminRepositorio.ObtenerUsuarios();
                return new ObtenerUsuariosResponse
                {
                    Exito = true,
                    Mensaje = "Usuarios obtenidos correctamente",
                    Usuarios = usuarios
                };
            }
            catch (Exception ex)
            {
                return new ObtenerUsuariosResponse
                {
                    Exito = false,
                    Mensaje = $"Error al obtener usuarios: {ex.Message}",
                };
            }
        }

        public bool CambiarRolUsuario(int idUsuario, string nuevoRol, out string mensaje)
        {
            try
            {
                bool resultado = _adminRepositorio.CambiarRolUsuario(idUsuario, nuevoRol);
                mensaje = resultado ? "Rol actualizado correctamente." : "No se pudo actualizar el rol.";
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al cambiar el rol: {ex.Message}";
                return false;
            }
        }

        public bool CambiarEstadoSuspension(int idUsuario, out string mensaje)
        {
            try
            {
                bool resultado = _adminRepositorio.CambiarEstadoSuspension(idUsuario);
                mensaje = resultado ? "Estado de suspensión actualizado correctamente." : "No se pudo actualizar el estado de suspensión.";
                return resultado;
            }
            catch (Exception ex)
            {
                mensaje = $"Error al cambiar el estado de suspensión: {ex.Message}";
                return false;
            }
        }
    }
}
