using MediatR;
using System;

namespace Identity.Application.Features.Auth.Commands
{
    public class ConfirmEmailCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}