using Moq;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace OnlineQueuingUnitTests
{
    public class AuthServiceTests
    {
        //private Mock<ApplicationContext> _authService;

        //public AuthServiceTests()
        //{
        //    _authService = new Mock<ApplicationContext> ();
        //}

        //private AuthService Subject()
        //{
        //    return new AuthService(_authService.Object);
        //}

        private string _authService()
        {
            var claims = new List<Claim>
            {
new Claim(ClaimTypes.Email, "balogh.botond8@gmail.com", ClaimValueTypes.String, "https://gov.uk")
            };
            ClaimsPrincipal user2 = new ClaimsPrincipal(new ClaimsIdentity(claims));
            Mock<AuthService> mockObject = new Mock<AuthService>();
            
            return mockObject.Setup(m => m.GetUserEmail(user2)).ToString();
        }

        [Fact]
        public void auth()
        {
            var claims = new List<Claim>
            {
new Claim(ClaimTypes.Email, "balogh.botond8@gmail.com", ClaimValueTypes.String, "https://gov.uk")
            };
            ClaimsPrincipal user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            //var service = new AuthService(applicationContext);
            //string userEmail = service.GetUserEmail(user2);
            //Assert.Equal(user.Email, userEmail);
            //Assert.Equal(user.Email, user2.Claims.First(claim => claim.Type == ClaimTypes.Email).Value);
            string valami = this._authService();
            Assert.Equal("balogh.botond8@gmail.com", valami);
        }
    }
}
