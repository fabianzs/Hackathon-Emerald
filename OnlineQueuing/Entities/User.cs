using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class User
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public List<Appointment> Appointments { get; set; }

        public User()
        {
            Appointments = new List<Appointment>();
        }
    }
}
