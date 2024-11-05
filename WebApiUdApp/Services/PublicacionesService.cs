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

        public void ReportarPublicacion(string token, ReportarPublicacionRequest request)
        {
            int idUsuarioReportador = ObtenerIdUsuarioDesdeToken(token);
            _publicacionesRepositorio.ReportarPublicacion(request.IdPublicacion, DateTime.Now, request.Motivo, idUsuarioReportador);
        }

        public void HacerPublicacion(string token, HacerPublicacionRequest request)
        {
            int idUsuario = ObtenerIdUsuarioDesdeToken(token);
            _publicacionesRepositorio.CrearPublicacion(idUsuario, request.Titulo, DateTime.Now);

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

        public bool ActualizarPublicacion(string token, ActualizarPublicacionRequest request)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioDesdeToken(token);
                return _publicacionesRepositorio.ActualizarPublicacion(idUsuario, request);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al actualizar la publicación: " + ex.Message);
                return false;
            }
        }

        public bool EliminarPublicacion(string token, int idPublicacion)
        {
            try
            {
                int idUsuario = ObtenerIdUsuarioDesdeToken(token);
                return _publicacionesRepositorio.EliminarPublicacion(idPublicacion, idUsuario);
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
        private int ObtenerIdUsuarioDesdeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            string sid = jsonToken?.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sid)?.Value;

            if (string.IsNullOrEmpty(sid))
            {
                throw new UnauthorizedAccessException("Token inválido.");
            }

            return int.Parse(sid);
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
