namespace SmsMicroservice.Core.Notifications.Events
{
    public class SmsSent
    {
        public string Message { get; }
        public SmsSent(string phoneNumber)
        {
            Message = $"Sms sent to phone number: {phoneNumber}";
        }
    }
}
