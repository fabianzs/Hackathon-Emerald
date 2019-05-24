﻿using Microsoft.AspNetCore.Http;
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
         //User FindAppointmentUser(AppointmentDTO appointmentDTO);

        // bool DeleteAppointment(HttpRequest request, AppointmentDTO appointmentDTO);
    }
}
