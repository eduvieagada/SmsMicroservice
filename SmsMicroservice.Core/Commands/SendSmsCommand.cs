namespace SmsMicroservice.Core.Commands
{
    public class SendSmsCommand
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
    }
}
