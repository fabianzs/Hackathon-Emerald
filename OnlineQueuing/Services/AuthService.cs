using Microsoft.AspNetCore.Http;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext applicationContext;

        public AuthService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            string userEmail = user.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            return userEmail;
        }

        public string GetUsername(ClaimsPrincipal user)
        {
            string username = user.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            return username;
        }

        public User SaveUser(string email, string username)
        {
            User user = applicationContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            if (user == null)
            {
                user = new User() { Email = email, Name = username, Role = Role.User };
                applicationContext.Users.Add(user);
                applicationContext.SaveChanges();
            }
            return user;
        }

        public string GetEmailFromJwtToken(HttpRequest request)
        {
            string tokenString = request.Headers["Authorization"];
            string token = tokenString.Split(" ")[1];
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            return jwtToken.Claims.First(claim => claim.Type == "email").Value;
        }
    }
}
