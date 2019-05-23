using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;

namespace OnlineQueuing.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly IAuthService authService;

        public UserController(ApplicationContext application, IAuthService authService)
        {
            this.authService = authService;
            this.applicationContext = application;
        }

            [HttpPost("appointment")]
             public IActionResult PostNewAppoinment(Appointment appointment)
             {
                List<User> appUsers = applicationContext.Users.Select(u => u).ToList();

                List<int> allTimeSlots = appUsers.SelectMany(u => u.Apointments.Select(a => a.TimeSlot)).ToList();
                List<DateTime> allDatetime = appUsers.SelectMany(d => d.Apointments.Select(t => t.Date.DateTime)).ToList();

                if ()
                {
                return Created("", new { message = "Success" });
                }
                return BadRequest( new { message = "Your request is not valid" });
             }
       

    }
}