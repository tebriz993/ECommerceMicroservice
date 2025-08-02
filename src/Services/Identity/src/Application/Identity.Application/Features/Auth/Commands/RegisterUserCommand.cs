using MediatR;
using Microsoft.AspNetCore.Identity; // For IdentityResult

namespace Identity.Application.Features.Auth.Commands
{
    public class RegisterUserCommand : IRequest<IdentityResult>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}