using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineQueuing.Entities;
using OnlineQueuing.Services;
using System.Threading.Tasks;

namespace OnlineQueuing.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [Authorize]
        [HttpGet("auth")]
        public IActionResult Authenticate()
        {
            string email = authService.GetUserEmail(User);
            string username = authService.GetUsername(User);
            authService.SaveUser(email, username);
            User user = authService.GetUserFromDb(authService.GetUserEmail(User));
            return Ok(authService.CreateJwtToken(user.Name, user.Email, user.Role));
        }

        [Authorize]
        [HttpGet("accesstoken")]
        public async Task<IActionResult> GetAccessToken()
        {
            string accessToken = string.Empty;
            if(User.Identity.IsAuthenticated)
            {
                accessToken = await HttpContext.GetTokenAsync("access_token");
            }
            return Ok(accessToken);
        }

        [Authorize]
        [HttpGet("refreshtoken")]
        public async Task<IActionResult> GetRefreshToken()
        {
            string refreshToken = string.Empty;
            if (User.Identity.IsAuthenticated)
            {
                refreshToken = await HttpContext.GetTokenAsync("refresh_token");
            }
            return Ok(refreshToken);
        }

        [Authorize("Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return Ok("You are an admin.");
        }
    }
}