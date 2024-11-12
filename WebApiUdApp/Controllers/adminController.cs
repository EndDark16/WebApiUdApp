using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("usuarios")]
        [Authorize(Roles = "Admin")]
        public IActionResult ObtenerUsuarios()
        {
            var response = _adminService.ObtenerUsuarios();
            if (response.Exito)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode(500, response);
            }
        }

        [HttpPut("usuarios/{idUsuario}/cambiar-rol")]
        [Authorize(Roles = "Admin")]
        public IActionResult CambiarRolUsuario(int idUsuario, [FromBody] string nuevoRol)
        {
            bool exito = _adminService.CambiarRolUsuario(idUsuario, nuevoRol, out string mensaje);
            if (exito)
            {
                return Ok(new { exito, mensaje });
            }
            else
            {
                return StatusCode(500, new { exito, mensaje });
            }
        }

        [HttpPut("usuarios/{idUsuario}/cambiar-estado-suspension")]
        [Authorize(Roles = "Admin")]
        public IActionResult CambiarEstadoSuspension(int idUsuario)
        {
            bool exito = _adminService.CambiarEstadoSuspension(idUsuario, out string mensaje);
            if (exito)
            {
                return Ok(new { exito, mensaje });
            }
            else
            {
                return StatusCode(500, new { exito, mensaje });
            }
        }
    }

}
