using System;

namespace EventBus.Messages.Events
{
    public class UserNeedsConfirmationEvent
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string ConfirmationToken { get; set; }
    }
}