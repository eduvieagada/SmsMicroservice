using System;
using System.Threading.Tasks;

namespace SmsMicroservice.Core.Messaging
{
    /// <summary>
    /// Abstraction to represent a connection to a message queue
    /// </summary>
    public interface IMessageQueue
    {
        public delegate Task AsyncEventHandler(IMessageQueue sender, QueueEventArgs eventArgs);
        /// <summary>
        /// Message Received event to be handled when a message is available on the queue
        /// </summary>
        public event AsyncEventHandler MessageReceived;
        public Task Publish<T>(T data);
    }
}
