using Microsoft.AspNetCore.Http;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface IAppointmentService
    {
         bool CreateAppointment(HttpRequest request, AppointmentDTO appointmentDTO);
         bool DeleteAppointment(Appointment appiontment, HttpRequest request);
    }
}
