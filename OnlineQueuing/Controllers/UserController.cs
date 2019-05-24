using Microsoft.AspNetCore.Mvc;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
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
        private IEmailSenderService emailService;
        private IAppointmentService appointmentService;

        public UserController(ISlackService ss,  IEmailSenderService es, IAppointmentService appointmentService)
        {
            this.slackService = ss;
            this.emailService = es;
            this.appointmentService = appointmentService;

        }

        //[HttpPost("appointment")]
        //public async Task<IActionResult> CreateAppointment([FromBody]AppointmentDTO appointmentDTO)
        //{
        //    foreach (var admin in appointmentService.GivesAllAdmin())
        //    {
        //        await slackService.SendSlackMessage(admin.Email, $"You have 1 new appointment with the following details: \n Timeslot: {appointmentDTO.TimeSlot}, \n ServiceType: {appointmentDTO.ServiceType}, \n Date: {appointmentDTO.Date}");
        //    }
        //    foreach (var admin in appointmentService.GivesAllAdmin())
        //    {
        //    await slackService.CreateSlackReminder(admin.Email, $"You have 1 new appointment with the following details: \n Timeslot: {appointmentDTO.TimeSlot}, \n ServiceType: {appointmentDTO.ServiceType}, \n Date: {appointmentDTO.Date}", appointmentDTO.TimeSlot, appointmentDTO.Date);
        //    }

        //    emailService.SendEmail(appointmentService.Email, appointmentService.FindAppointmentUser(appointmentDTO).Name);
        //    return Created("", new { messageSentTo = "message" });
        //}
    }
}