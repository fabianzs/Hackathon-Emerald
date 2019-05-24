using Moq;
using OnlineQueuing.Controllers;
using OnlineQueuing.Data;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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
                Apointments = new List<Appointment>(),
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

        [Fact]
        public void GetUserFromDb_ReturnsUser()
        {
            Assert.Equal(testUser, authService.Object.GetUserFromDb("zombilyanos@gmail.com"));
        }

        //private Mock<ApplicationContext> CreateDbContext()
        //{
        //    var persons = GetFakeData().AsQueryable();
        //    var dbSet = new Mock<DbSet<Person>>();
        //    dbSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(persons.Provider);
        //    dbSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(persons.Expression);
        //    dbSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(persons.ElementType);
        //    dbSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(persons.GetEnumerator());
        //    var context = new Mock<ApplicationDbContext>();
        //    context.Setup(c => c.Persons).Returns(dbSet.Object);
        //    return context;
        //}

        //private IEnumerable<Person> GetFakeData()
        //{
        //    var i = 1;
        //    var persons = A.ListOf<Person>(26);
        //    persons.ForEach(x => x.Id = i++);
        //    return persons.Select(_ => _);
        //}
    }
}
