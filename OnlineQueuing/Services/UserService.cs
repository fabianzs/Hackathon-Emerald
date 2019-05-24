using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public class UserService : IUserService
    {
        private readonly IAppointmentService appointmentService;
        private readonly ISlackService slackService;
        private readonly IEmailSenderService emailService;


        public UserService(IAppointmentService appointmentService, ISlackService slackService, IEmailSenderService emailSenderService)
        {
            this.appointmentService = appointmentService;
            this.slackService = slackService;
            this.emailService = emailSenderService;
        }

        public async Task SendMessageToAdmin(Appointment appointment)
        {
            foreach (var admin in appointmentService.GivesAllAdmin())
            {
                await slackService.SendSlackMessage(admin.Email, $"You have 1 new appointment with the following details: \n Timeslot: {appointment.TimeSlot}, \n ServiceType: {appointment.ServiceType}, \n Date: {appointment.Date}");
            }
            foreach (var admin in appointmentService.GivesAllAdmin())
            {
                await slackService.CreateSlackReminder(admin.Email, $"You have 1 new appointment with the following details: \n Timeslot: {appointment.TimeSlot}, \n ServiceType: {appointment.ServiceType}, \n Date: {appointment.Date}", appointment.TimeSlot, appointment.Date);
            }

            emailService.SendEmail(appointment.User.Email, appointment.User.Name);
        }
    }
}
