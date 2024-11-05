using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using WebApiUdApp.Dtos;
using WebApiUdApp.Dtos.Request.ComentarioRequest;
using WebApiUdApp.Dtos.Response.ComentarioResponse;
using WebApiUdApp.Repositories;

namespace WebApiUdApp.Services
{
    public class ComentarioService
    {
        private readonly ComentarioRepositorio _comentarioRepositorio;

        public ComentarioService()
        {
            _comentarioRepositorio = new ComentarioRepositorio();
        }

        public IEnumerable<ComentarioDto> ObtenerComentariosPorPublicacion(int idPublicacion)
        {
            try
            {
                return _comentarioRepositorio.GetComentariosByPublicacionId(idPublicacion);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentarios por publicación: " + ex.Message);
                return new List<ComentarioDto>();
            }
        }

        public ComentarioDto? ObtenerComentarioPorId(int idComentario)
        {
            try
            {
                return _comentarioRepositorio.GetComentarioById(idComentario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentario por ID: " + ex.Message);
                return null;
            }
        }

        public ActualizarComentarioResponse ActualizarComentario(string token, ActualizarComentarioRequest comentarioActualizar)
        {
            try
            {
                ComentarioDto comentarioDtoActualizar = new ComentarioDto
                {
                    IdComentario = comentarioActualizar.IdComentario,
                    Contenido = comentarioActualizar.Contenido,
                    FechaCreacion = DateTime.Now,
                    IdUsuario = ObtenerIdUsuarioDesdeToken(token),
                    IdPublicacion = comentarioActualizar.IdPublicacion
                };
                bool Actualizado = _comentarioRepositorio.UpdateComentario(comentarioDtoActualizar);
                if (!Actualizado)
                {
                    return new ActualizarComentarioResponse
                    {
                        Success = false,
                        Message = "Error en el repositorio al actualizar el comentario"
                    };
                }

                // Obtener los datos del comentario actualizado para incluirlos en la respuesta
                var comentarioActualizado = _comentarioRepositorio.GetUltimoComentarioRealizado(comentarioDtoActualizar.IdUsuario, comentarioDtoActualizar.IdPublicacion);

                return new ActualizarComentarioResponse
                {
                    Success = true,
                    Message = "Comentario actualizado exitosamente.",
                    ComentarioActualizado = comentarioActualizado
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al actualizar el comentario: " + ex.Message);
                return new ActualizarComentarioResponse
                {
                    Success = false,
                    Message = "Error en el servicio de Actualizar el comentario"
                };
            }
        }

        public bool EliminarComentario(int idComentario)
        {
            try
            {
                return _comentarioRepositorio.DeleteComentario(idComentario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el comentario: " + ex.Message);
                return false;
            }
        }

        public CrearComentarioResponse AgregarComentario(string token, CrearComentarioRequest comentarioNuevo)
        {
            ComentarioDto comentarioDtoNuevo = new ComentarioDto
            {
                IdUsuario = ObtenerIdUsuarioDesdeToken(token),
                IdPublicacion = comentarioNuevo.IdPublicacion,
                Contenido = comentarioNuevo.Comentario,
                FechaCreacion = DateTime.Now
            };

            bool Creado = _comentarioRepositorio.AddComentario(comentarioDtoNuevo);
            if (!Creado)
            {
                return new CrearComentarioResponse
                {
                    Exito = false,
                    Mensaje = "Error al crear el comentario."
                };
            }

            // Obtener los datos del comentario creado para incluirlos en la respuesta
            var comentarioCreado = _comentarioRepositorio.GetUltimoComentarioRealizado(comentarioDtoNuevo.IdUsuario, comentarioDtoNuevo.IdPublicacion);

            return new CrearComentarioResponse
            {
                Exito = true,
                Mensaje = "Comentario creado exitosamente.",
                Comentario = comentarioCreado
            };
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

        public List<ComentarioDto> ObtenerComentariosPorUsuario(int idUsuario)
        {
            try
            {
                return _comentarioRepositorio.ObtenerComentariosPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener comentarios por usuario: " + ex.Message);
                return new List<ComentarioDto>();
            }
        }
    }
}
