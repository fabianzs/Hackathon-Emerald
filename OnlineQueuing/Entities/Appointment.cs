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
        public ServiceType ServiceType { get; set; }
        public Date Date { get; set; }
        public int AppointmentTimeSlot { get; set; }
    }
}
