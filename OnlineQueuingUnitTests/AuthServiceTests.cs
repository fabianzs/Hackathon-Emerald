using Moq;
using OnlineQueuing.Controllers;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;

namespace OnlineQueuingUnitTests
{
    public class AuthServiceTests
    {
        private Mock<IAuthService> authService;
        private AuthController authController;
        private ClaimsPrincipal claims;
        private User testUser;

        public AuthServiceTests()
        {
            string email = "zombilyanos@gmail.com";
            string name = "zombilyani";
            testUser = new User()
            {
                Name = name,
                Email = email,
                Role = "User",
            };
            claims = new ClaimsPrincipal(new List<ClaimsIdentity>() { new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Name, "zombilyan"), new Claim(ClaimTypes.Email, "zombilyanos@gmail@gmail.com"), new Claim(ClaimTypes.Role, "User") }) });
            this.authService = new Mock<IAuthService>();
            authService.Setup(x => x.GetUserEmail(claims)).Returns(email);
            authService.Setup(x => x.GetUsername(claims)).Returns(name);
            authService.Setup(x => x.GetUserFromDb(email)).Returns(testUser);
            authController = new AuthController(authService.Object);
        }

        [Fact]
        public void GetUserEmail_ReturnsEmail()
        {
            Assert.Equal("zombilyanos@gmail.com", authService.Object.GetUserEmail(claims));
        }

        [Fact]
        public void GetUserName_ReturnsUsername()
        {
            Assert.Equal("zombilyani", authService.Object.GetUsername(claims));
        }

        [Fact]
        public void GetUserFromDb_ReturnsUser()
        {
            Assert.Equal(testUser, authService.Object.GetUserFromDb("zombilyanos@gmail.com"));
        }
    }
}
