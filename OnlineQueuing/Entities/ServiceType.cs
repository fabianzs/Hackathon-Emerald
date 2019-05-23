using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class ServiceType
    {
        public long ServiceTypeId { get; set; }
        public string Type { get; set; }
        public Business Business { get; set; }
        public int MaxAppointmentsPerDay { get; set; }
        public int[] TimeSlots { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<ServiceDate> ServiceDates { get; set; }
    }
}
