using System;

namespace SmsMicroservice.Core.Messaging
{
    /// <summary>
    /// Custom Event Args for Message Queues
    /// </summary>
    public class QueueEventArgs : EventArgs
    {
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public bool ReDelivered { get; set; }
        public string ConsumerTag { get; set; }
        public ReadOnlyMemory<byte> Body { get; set; }
        public string DeliveryTag { get; set; }
    }
}
