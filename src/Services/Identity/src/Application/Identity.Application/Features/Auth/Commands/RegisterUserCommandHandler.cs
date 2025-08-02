using AutoMapper;
using EventBus.Messages.Events; // The event we just created
using Identity.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Identity.Application.Features.Auth.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _userManager = userManager;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AppUser>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Add the user to a default "Customer" role
                await _userManager.AddToRoleAsync(user, "Customer");

                // 1. Generate the email confirmation token
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // 2. Publish the event to RabbitMQ
                await _publishEndpoint.Publish(new UserNeedsConfirmationEvent
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    ConfirmationToken = token
                }, cancellationToken);
            }

            return result;
        }
    }
}