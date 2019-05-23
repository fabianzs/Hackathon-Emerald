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
        private readonly IAppointmentService appointmentService;
        private readonly IAuthService authService;

        public AppointmentController(IAppointmentService appointmentService, IAuthService authService)
        {
            this.authService = authService;
            this.appointmentService = appointmentService;
        }

        [HttpPost("appointment")]
        public IActionResult PostNewAppointment(Appointment appointment)
        {
            bool result = appointmentService.CreateAppointment(appointment);

            if (result)
            {
                return Created("", new { message = "Success" });
            }
            else
            {
                return BadRequest(new { message = "Your request is not valid" });
            }
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