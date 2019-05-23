using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.DTO
{
    public class AppointmentDTO
    {
        public int TimeSlot { get; set; }
        public string ServiceType { get; set; }
        public string Date { get; set; }
    }


}
