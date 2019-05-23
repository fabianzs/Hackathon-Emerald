using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Helpers
{

    public class AdminRequirement : IAuthorizationRequirement
    {
        public string Role { get; }

        public AdminRequirement ()
        {
            Role = "Admin";
        }
    }
}
