using Microsoft.Extensions.Configuration;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Mocks
{
    public class MockAuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public MockAuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetUserEmail()
        {
            return null;
        }

        public string GetUsername()
        {
            return null;
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
