using System;
using System.Collections.Generic;

namespace OnlineQueuing.Entities
{
    public class Date
    {
        public long DateId { get; set; }
        public DateTime DateTime { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Date()
        {
            this.Appointments = new List<Appointment>();
        }
    }
}
