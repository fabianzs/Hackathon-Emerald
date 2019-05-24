using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static OnlineQueuing.Helpers.CustomException;

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
            if(user == null)
            {
                throw new Exception();
            }
            string userEmail = user.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            if (userEmail == null)
            {
                throw new Exception();
            }
            return userEmail;
        }

        public string GetUsername(ClaimsPrincipal user)
        {
            if (user == null)
            {
                throw new MissingClaimsException();
            }
            string username = user.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            if (username == null)
            {
                throw new MissingUsernameException();
            }
            return username;
        }

        public User SaveUser(string email, string username)
        {
            if (email == null || username == null)
            {
                throw new MissingInformationException();
            }
            User user = applicationContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            if (user == null)
            {
                user = new User()
                {
                    Email = email,
                    Name = username,
                    Role = Role.Admin
                };
                applicationContext.Users.Add(user);
                applicationContext.SaveChanges();
            }

            return user;
        }

        public User GetUserFromDb(string email)
        {
            User user = applicationContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            if(user == null)
            {
                throw new UserNotExistException();
            }
            return user;
        }

        public string CreateJwtToken(string name, string email, string role)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                claims: new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                },
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:Jwt:Secret"])), SecurityAlgorithms.HmacSha256Signature)
                );
            string securetoken = new JwtSecurityTokenHandler().WriteToken(token);

            if(securetoken == null)
            {
                throw new TokenGenerationException();
            }

            return securetoken;
        }

        public string GetEmailFromJwtToken(HttpRequest request)
        {
            string tokenString = request.Headers["Authorization"];
            string token = tokenString.Split(" ")[1];
            JwtSecurityToken jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            string email = jwtToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;

            if(email == null)
            {
                throw new MissingUserEmailException();
            }

            return email;
        }
    }
}
