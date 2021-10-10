using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMicroservice.Core.Notifications
{
    /// <summary>
    /// Abstraction for Broadcasting notifications
    /// </summary>
    public interface INotificationBus
    {
        /// <summary>
        /// Broadcast data to multiple subscribers
        /// </summary>
        /// <typeparam name="T">Message Type</typeparam>
        /// <param name="data">Message to be broadcasted</param>
        /// <returns>Completed Task</returns>
        public Task BroadCast<T>(T data);
    }
}
