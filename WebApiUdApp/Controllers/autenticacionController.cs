using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApiUdApp.Services;

namespace WebApiUdApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutenticacionController : ControllerBase
    {
        [HttpPost("AdminRol")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminRol()
        {
            return Ok(new

            {
                Mensaje = "Autenticado como Admin"
            });
        }
        [HttpPost("ModeradorRol")]
        [Authorize(Roles = "Moderador")]
        public IActionResult ModeradorRol()
        {
            return Ok(new

            {
                Mensaje = "Autenticado como Moderador"
            });
        }
        [HttpPost("GeneralRol")]
        [Authorize(Roles = "General")]
        public IActionResult GeneralRol()
        {
            return Ok(new

            {
                Mensaje = "Autenticado como General"
            });
        }
        [HttpPost("GerenteRol")]
        [Authorize(Roles = "Gerente")]
        public IActionResult GerenteRol()
        {
            return Ok(new

            {
                Mensaje = "Autenticado como Gerente"
            });
        }
    }
}
