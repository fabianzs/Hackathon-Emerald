using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineQueuing.Data;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;


namespace OnlineQueuing.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService appointmentService;
        

        public AppointmentController(IAppointmentService appointmentService)
        {
            
            this.appointmentService = appointmentService;
        }

        [HttpPost("appointment")]
        public IActionResult PostNewAppointment([FromBody]AppointmentDTO appointmentDTO)
        {
     
            bool result = appointmentService.CreateAppointment(Request, appointmentDTO);

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
            bool result = appointmentService.DeleteAppointment(appointment,Request);

            if (result)
            {
                return NoContent();
            }
            return BadRequest(new { message = "Your request is not valid" });
        }
    }
}