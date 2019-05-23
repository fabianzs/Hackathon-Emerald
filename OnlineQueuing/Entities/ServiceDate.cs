using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Entities
{
    public class ServiceDate
    {
        public long ServiceTypeId { get; set; }
        public long DataId { get; set; }
        public ServiceDate ServiceType { get; set; }
        public Date Date { get; set; }

    }
}
