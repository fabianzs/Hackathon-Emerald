using Microsoft.Extensions.Configuration;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System.Security.Claims;

namespace OnlineQueuing.Mocks
{
    public class MockAuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public MockAuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            return "user";
        }

        public string GetUsername(ClaimsPrincipal user)
        {
            return "user";
        }

        public User SaveUser(string email, string username)
        {
            return null;
        }

        public User GetUserFromDb(string email)
        {
            return null;
        }

        public string CreateJwtToken(string name, string email, string role)
        {
            return "1234";
        }
    }
}
