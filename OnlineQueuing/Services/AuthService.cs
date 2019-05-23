using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQueuing.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly ApplicationContext applicationContext;

        public AuthService(IConfiguration configuration, ApplicationContext applicationContext)
        {
            this.configuration = configuration;
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

        public User GetUserFromDb(string email)
        {
            User user = applicationContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            return user;
        }

        public string CreateJwtToken(string name, string email, string role)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                claims: new Claim[] { new Claim("Name", name), new Claim("Email", email), new Claim("Role", role) },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Secret"])), SecurityAlgorithms.HmacSha256Signature)
                );
            string securetoken = new JwtSecurityTokenHandler().WriteToken(token);

            return securetoken;
        }
    }
}
