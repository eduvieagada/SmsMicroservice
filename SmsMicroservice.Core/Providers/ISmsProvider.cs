using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsMicroservice.Core.Providers
{
    /// <summary>
    /// Abstraction for sending sms messages
    /// </summary>
    public interface ISmsProvider
    {
        /// <summary>
        /// Send sms to a given phonenumber
        /// </summary>
        /// <param name="sms"></param>
        /// <returns>True if message is successful otherwise false</returns>
        public Task<bool> Send(string sms, string phoneNumber);
    }
}
