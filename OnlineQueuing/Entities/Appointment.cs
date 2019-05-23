using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class Appointment
    {
        public long AppointmentId { get; set; }
        public int TimeSlot { get; set; }
        public string ServiceType { get; set; }
        public string Date { get; set; }
    }
}
