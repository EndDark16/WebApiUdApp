using System.ComponentModel.DataAnnotations;

namespace WebApiUdApp.Dtos.Request.UserRequest
{
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 8)] 
        public string Contrasena { get; set; } = string.Empty;
    }
}