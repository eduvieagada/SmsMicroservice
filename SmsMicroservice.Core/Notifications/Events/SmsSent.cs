using System;

namespace SmsMicroservice.Core.Notifications.Events
{
    public class SmsSent
    {
        public string Message { get; }
        private DateTime CreatedAt { get;  }
        public SmsSent(string phoneNumber)
        {
            CreatedAt = DateTime.Now;
            Message = $"Sms sent to phone number: {phoneNumber} at {CreatedAt}";
        }
    }
}
