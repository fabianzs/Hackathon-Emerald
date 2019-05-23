using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace OnlineQueuing.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IAuthService authService;

        public AppointmentService(ApplicationContext applicationContext, IAuthService authService)
        {
            this.applicationContext = applicationContext;
            this.authService = authService;
        }

        public bool CreateAppointment(Appointment appiontment, HttpRequest request)
        {
            List<User> appUsers = applicationContext.Users.Select(u => u).ToList();
            List<int> allTimeSlots = appUsers.SelectMany(u => u.Apointments.Select(a => a.TimeSlot)).ToList();
            List<DateTime> allDatetime = appUsers.SelectMany(d => d.Apointments.Select(t => t.Date.DateTime)).ToList();

            if (!allTimeSlots.Contains(appiontment.TimeSlot) && appiontment.TimeSlot > 1 && appiontment.TimeSlot < 8 && !allDatetime.Contains(appiontment.Date.DateTime))
            {
                string email = authService.GetEmailFromJwtToken(request);
                User user = applicationContext.Users.FirstOrDefault(u => u.Email == email);
                user.Apointments.Add(appiontment);
                applicationContext.Add(appiontment);
                applicationContext.SaveChanges();
                return true;
            }
            else return false;
        }

        public bool DeleteAppointment(Appointment appiontment, HttpRequest request)
        {
            string email = authService.GetEmailFromJwtToken(request);
            User user = applicationContext.Users.FirstOrDefault(u=>u.Email==email);

            List<Appointment> userAppointments = user.Apointments.Select(a => a).ToList();

            if (userAppointments.Contains(appiontment))
            {
                applicationContext.Remove(appiontment);
                applicationContext.SaveChanges();
                return true;
            }
            else return false;
        }
    }
}
