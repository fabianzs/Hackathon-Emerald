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

        public UserController(ISlackService ss)
        {
            this.slackService = ss;
        }

        [HttpPost("appointment")]
        public async Task<IActionResult> CreateAppointment()
        {

            await slackService.SendSlackMessage("laszlo.molnar25@gmail.com", "You have 1 new appointment!" );
            await slackService.CreateSlackReminder("laszlo.molnar25@gmail.com", "Reminder set!", "a");
            return Created("", new { messageSentTo = "message" });
        }
    }
}
