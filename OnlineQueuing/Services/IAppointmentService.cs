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
         Appointment CreateAppointment(HttpRequest request, AppointmentDTO appointmentDTO);
         List<User> GivesAllAdmin();
         bool DeleteAppointment(long id,HttpRequest request);
        //User FindAppointmentUser(AppointmentDTO appointmentDTO);
    }
}
