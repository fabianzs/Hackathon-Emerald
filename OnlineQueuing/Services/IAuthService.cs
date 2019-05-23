using Microsoft.AspNetCore.Http;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public interface IAuthService
    {
        string GetUserEmail(ClaimsPrincipal user);
        string GetUsername(ClaimsPrincipal user);
        User SaveUser(string email, string username);
        string GetEmailFromJwtToken(HttpRequest request);
    }
}
