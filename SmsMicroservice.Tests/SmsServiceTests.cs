using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SmsMicroservice.Core;
using SmsMicroservice.Core.Commands;
using SmsMicroservice.Core.Messaging;
using SmsMicroservice.Core.Notifications;
using SmsMicroservice.Core.Notifications.Events;
using SmsMicroservice.Core.Providers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmsMicroservice.Tests
{
    using static It;
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SmsShouldBeSentToProvider_AndEventShouldBeBroadCasted()
        {
            var mockNotificationBus = new Mock<INotificationBus>();
            mockNotificationBus.Setup(m => m.BroadCast(IsAny<SmsSent>())).Returns(Task.CompletedTask);

            var mockLogger = new Mock<ILogger<SmsService>>();

            var mockSmsProvider = new Mock<ISmsProvider>();
            mockSmsProvider.Setup(m => m.Send(IsAny<string>(), IsAny<string>())).ReturnsAsync(true);

            var messageQueue = new TestQueue();

            var smsService = new SmsService(mockLogger.Object, messageQueue, mockNotificationBus.Object, mockSmsProvider.Object);
            smsService.InitializeService();

            var msgBody = JsonSerializer.SerializeToUtf8Bytes(new SendSmsCommand
            {
                PhoneNumber = "+2348148657415",
                Message = "Test message"
            });

            await messageQueue.OnMessageReceived(new QueueEventArgs
            {
                DeliveryTag = "Test Tag",
                ReDelivered = false,
                Body = msgBody,
                ConsumerTag = "Test consumer",
                Exchange = "Test Exchange",
                RoutingKey = "Test key"
            });

            mockSmsProvider.Verify(v => v.Send(IsAny<string>(), IsAny<string>()), Times.Once);
            mockNotificationBus.Verify(v => v.BroadCast(IsAny<SmsSent>()), Times.Once);
        }
    }

    public class TestQueue : IMessageQueue
    {
        public event IMessageQueue.AsyncEventHandler MessageReceived;

        public Task Publish<T>(T data)
        {
            return Task.CompletedTask;
        }

        public async Task OnMessageReceived(QueueEventArgs eventArgs)
        {
            await MessageReceived(this, eventArgs);
        }
    }
}