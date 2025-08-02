using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Web; 

namespace Notification.Worker.Consumers
{
    public class UserNeedsConfirmationConsumer : IConsumer<UserNeedsConfirmationEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public UserNeedsConfirmationConsumer(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<UserNeedsConfirmationEvent> context)
        {
            var user = context.Message;

            var urlEncodedToken = HttpUtility.UrlEncode(user.ConfirmationToken);

            var confirmationLink = $"{_configuration["ApiSettings:IdentityApiUrl"]}/api/v1/Auth/confirm-email?userId={user.UserId}&token={urlEncodedToken}";

            var subject = "Confirm your Email Address";
            var body = $@"
                <h1>Welcome, {user.FirstName}!</h1>
                <p>Please confirm your email address by clicking the link below:</p>
                <a href='{confirmationLink}'>Confirm My Email</a>
                <p>If you did not create an account, no further action is required.</p>";

            await _emailService.SendEmailAsync(user.Email, subject, body);
        }
    }
}