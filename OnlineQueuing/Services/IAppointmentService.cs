using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface IAppointmentService
    {
         bool CreateAppointment(Appointment appiontment);
    }
}
