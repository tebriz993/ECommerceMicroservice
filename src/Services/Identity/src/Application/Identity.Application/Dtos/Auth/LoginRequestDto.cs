using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Dtos.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}