using Microsoft.Extensions.Logging;
using SmsMicroservice.Core.Commands;
using SmsMicroservice.Core.Messaging;
using SmsMicroservice.Core.Notifications;
using SmsMicroservice.Core.Notifications.Events;
using SmsMicroservice.Core.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmsMicroservice.Core
{
    public class SmsService
    {
        private readonly ILogger<SmsService> _logger;
        private readonly IMessageQueue _messageQueue;
        private readonly INotificationBus _notificationBus;
        private readonly ISmsProvider _smsProvider;

        public SmsService(ILogger<SmsService> logger, IMessageQueue messageHandler, INotificationBus notificationBus, ISmsProvider smsProvider)
        {
            _logger = logger;
            _messageQueue = messageHandler;
            _notificationBus = notificationBus;
            _smsProvider = smsProvider;
        }

        public void InitializeService()
        {
            _messageQueue.MessageReceived += MessageHandler_MessageReceived;
        }

        private async Task MessageHandler_MessageReceived(IMessageQueue sender, QueueEventArgs eventArgs)
        {
            try
            {
                _logger.LogInformation($"Sms Command Received from {eventArgs.Exchange}");
                byte[] message = eventArgs.Body.ToArray();
                var text = Encoding.UTF8.GetString(message);

                _logger.LogDebug($"SmsCommand: {text}");

                var smsCommand = JsonSerializer.Deserialize<SendSmsCommand>(text);

                await SendSms(smsCommand);
                _notificationBus.BroadCast(new SmsSent(smsCommand.PhoneNumber));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private async Task SendSms(SendSmsCommand cmd)
        {
            bool smsSent = await _smsProvider.Send(cmd.Message, cmd.PhoneNumber);

            if (!smsSent)
            {
                await _messageQueue.Publish(cmd);
            }
        }

    }
}
