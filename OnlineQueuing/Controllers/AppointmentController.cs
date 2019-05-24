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
        private readonly IUserService userService;
        

        public AppointmentController(IAppointmentService appointmentService, IUserService userService)
        {
            this.appointmentService = appointmentService;
            this.userService = userService;
        }

        [HttpPost("createappointment")]
        public async Task<IActionResult> PostNewAppointment([FromBody]AppointmentDTO appointmentDTO)
        {
     
            Appointment newAppointment = appointmentService.CreateAppointment(Request, appointmentDTO);

            if (newAppointment != null)
            {
                await userService.SendMessages(newAppointment);
                return Created("", new { message = "Success" });
            }
            else
            {
                return BadRequest(new { message = "Your request is not valid" });
            }
        }
        /*
                [HttpDelete("deleteAppointment")]
                public IActionResult DeleteAppointment([FromBody]AppointmentDTO appointmentDTO)
                {
                    bool result = appointmentService.DeleteAppointment(Request,appointmentDTO);

                    if (result)
                    {
                        return NoContent();
                    }
                    return BadRequest(new { message = "Your request is not valid" });           
                }
                */
    }
}