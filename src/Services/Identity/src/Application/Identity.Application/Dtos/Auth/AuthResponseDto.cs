namespace Identity.Application.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}