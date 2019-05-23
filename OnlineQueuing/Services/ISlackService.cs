using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface ISlackService
    {
        Task SendEmail(string email, string messageToSend);
    }
}
