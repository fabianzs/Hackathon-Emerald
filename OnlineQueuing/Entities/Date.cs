using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class Date
    {
        public long DateId { get; set; }
        public DateTime DateTime { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
