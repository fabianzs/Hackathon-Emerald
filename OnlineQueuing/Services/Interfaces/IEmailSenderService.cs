using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface IEmailSenderService
    {
        void SendEmail(string toEmail, string toName);

    }
}
