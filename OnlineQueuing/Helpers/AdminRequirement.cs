using Microsoft.AspNetCore.Authorization;

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
