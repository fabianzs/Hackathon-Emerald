using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;

namespace OnlineQueuing.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationContext applicationContext;
        private readonly IAuthService authService;

        public AppointmentController(ApplicationContext application, IAuthService authService)
        {
            this.authService = authService;
            this.applicationContext = application;
        }
 
        [HttpPost("appointment")]
        public IActionResult PostNewAppointment(Appointment appointment)
        {
            List<User> appUsers = applicationContext.Users.Select(u => u).ToList();

            List<int> allTimeSlots = appUsers.SelectMany(u => u.Apointments.Select(a => a.TimeSlot)).ToList();
            List<DateTime> allDatetime = appUsers.SelectMany(d => d.Apointments.Select(t => t.Date.DateTime)).ToList();

            if (!allTimeSlots.Contains(appointment.TimeSlot) && appointment.TimeSlot > 1 && appointment.TimeSlot < 8 && !allDatetime.Contains(appointment.Date.DateTime))
            {
                applicationContext.Add(appointment);
                applicationContext.SaveChanges();

                return Created("", new { message = "Success" });
            }
            return BadRequest( new { message = "Your request is not valid" });
        }

        [HttpDelete("deleteAppointment")]
        public IActionResult DeleteAppointment(Appointment appointment)
        {
            List<User> appUsers = applicationContext.Users.Select(u => u).ToList();
            List<Appointment> allAppointments = appUsers.SelectMany(u => u.Apointments).ToList();

            if (allAppointments.Contains(appointment))
            {
                applicationContext.Remove(appointment);
                applicationContext.SaveChanges();
                return NoContent();
            }
            return BadRequest(new { message = "Your request is not valid" });
        }
    }
}