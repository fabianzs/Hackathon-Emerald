using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationContext applicationContext;

        public AppointmentService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public void CreateAppointment(Appointment appiontment)
        {
            List<User> appUsers = applicationContext.Users.Select(u => u).ToList();
            List<int> allTimeSlots = appUsers.SelectMany(u => u.Apointments.Select(a => a.TimeSlot)).ToList();
            List<DateTime> allDatetime = appUsers.SelectMany(d => d.Apointments.Select(t => t.Date.DateTime)).ToList();

            if (!allTimeSlots.Contains(appiontment.TimeSlot) && appiontment.TimeSlot > 1 && appiontment.TimeSlot < 8 && !allDatetime.Contains(appiontment.Date.DateTime))
            {
                applicationContext.Add(appiontment);
                applicationContext.SaveChanges();
            }
        }
    }
}
