using OnlineQueuing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface ISlackService
    {
        Task<EmailLookupResponse>EmailLookup(string email);
        Task SendSlackMessage(string email, string messageToSend);
        Task CreateSlackReminder(string email, string notificationBody, int timeSlot, string day);
    }
}
