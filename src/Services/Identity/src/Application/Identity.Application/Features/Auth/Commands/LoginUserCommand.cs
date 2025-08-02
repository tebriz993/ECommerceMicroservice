using Identity.Application.Dtos.Auth;
using MediatR;

namespace Identity.Application.Features.Auth.Commands
{
    public class LoginUserCommand : IRequest<AuthResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}