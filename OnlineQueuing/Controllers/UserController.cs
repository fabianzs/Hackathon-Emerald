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
    }
}