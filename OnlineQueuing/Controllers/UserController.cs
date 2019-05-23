using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Controllers
{
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateAppointment()
        {
            
            return Created("", new { messageSentTo = "message" });
        }
    }
}
