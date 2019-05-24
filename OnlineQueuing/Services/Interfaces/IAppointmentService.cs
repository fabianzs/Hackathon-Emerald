using Microsoft.AspNetCore.Http;
using OnlineQueuing.DTO;
using OnlineQueuing.Entities;
using System.Collections.Generic;

namespace OnlineQueuing.Services
{
    public interface IAppointmentService
    {
         Appointment CreateAppointment(HttpRequest request, AppointmentDTO appointmentDTO);
         List<User> GivesAllAdmin();
    }
}
