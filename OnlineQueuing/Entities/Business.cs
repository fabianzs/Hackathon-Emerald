using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class Business
    {
        public long BusinessId { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Email { get; set; }
        public ICollection<ServiceType> ServiceTypes { get; set; }

    }
}
