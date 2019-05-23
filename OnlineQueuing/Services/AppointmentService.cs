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

        }


    }
}
