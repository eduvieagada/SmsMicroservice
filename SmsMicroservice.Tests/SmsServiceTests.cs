using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SmsMicroservice.Core.Messaging;
using SmsMicroservice.Core.Notifications;
using SmsMicroservice.Core.Providers;

namespace SmsMicroservice.Tests
{
    public class Tests
    {
        private ILogger _logger;
        private IMessageQueue _messageQueue;
        private INotificationBus _notificationBus;
        private ISmsProvider _smsProvider;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}