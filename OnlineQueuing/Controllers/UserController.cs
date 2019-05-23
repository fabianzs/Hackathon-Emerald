using Microsoft.AspNetCore.Mvc;
using OnlineQueuing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Controllers
{
    public class UserController : Controller
    {
        private ISlackService slackService;
        private EmailSenderService emailService;
        private EmailSenderService emailService2;

        public UserController(ISlackService ss, EmailSenderService es, EmailSenderService es2)
        {
            this.slackService = ss;
            this.emailService = es;
            this.emailService2 = es2;
        }

        [HttpPost("appointment")]
        public async Task<IActionResult> CreateAppointment()
        {
            await slackService.SendSlackMessage("laszlo.molnar25@gmail.com", "You have 1 new appointment!");
            await slackService.CreateSlackReminder("laszlo.molnar25@gmail.com", "Reminder set!", "a");
            emailService2.SendEmail("balogh.botond8@gmail.com", "Boti");
            return Created("", new { messageSentTo = "message" });
        }
    }
}
