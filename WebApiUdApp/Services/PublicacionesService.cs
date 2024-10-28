using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApiUdApp.Dtos.Request.PublicacionesRequest;
using WebApiUdApp.Dtos;

namespace WebApiUdApp.Services
{
    public class PublicacionesService
    {
        private readonly PublicacionesRepositorio _publicacionesRepositorio;

        public PublicacionesService(PublicacionesRepositorio publicacionesRepositorio)
        {
            _publicacionesRepositorio = publicacionesRepositorio;
        }

        public void ReportarPublicacion(ReportarPublicacionRequest request)
        {
            _publicacionesRepositorio.ReportarPublicacion(request.IdPublicacion, DateTime.Now, request.Motivo, request.IdUusuarioReportador);
        }

        public void HacerPublicacion(HacerPublicacionRequest request)
        {
            _publicacionesRepositorio.CrearPublicacion(request.Titulo, DateTime.Now, request.IdUsuario);
        }
        public PublicacionDto ObtenerPublicacionPorId(int id)
        {
            try
            {
                return _publicacionesRepositorio.ObtenerPublicacionPorId(id);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al obtener la publicación: " + ex.Message);
                return null;
            }
        }

        public bool ActualizarPublicacion(ActualizarPublicacionRequest request)
        {
            try
            {
                return _publicacionesRepositorio.ActualizarPublicacion(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al actualizar la publicación: " + ex.Message);
                return false;
            }
        }

        public bool EliminarPublicacion(int id)
        {
            try
            {
                return _publicacionesRepositorio.EliminarPublicacion(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al eliminar la publicación: " + ex.Message);
                return false;
            }
        }
    public void ToggleLike(ToggleLikeRequest request)
        {
            if (request.LikeStatus)
            {
                _publicacionesRepositorio.GuardarLike(request.IdUsuario, request.IdPublicacion);
            }
            else
            {
                _publicacionesRepositorio.EliminarLike(request.IdUsuario, request.IdPublicacion);
            }
        }

        public IEnumerable<PublicacionDto> ObtenerPublicacionesRecientes(ClaimsPrincipal user)
        {
            var identity = user.Identity as ClaimsIdentity;
            if (identity == null) throw new UnauthorizedAccessException("Usuario no autenticado");

            var userIdClaim = identity.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sid);
            if (userIdClaim == null) throw new UnauthorizedAccessException("El token no contiene un IdUsuario válido");

            int userId = int.Parse(userIdClaim.Value);
            return _publicacionesRepositorio.PublicacionesRecientes(userId);
        }
    }

}
