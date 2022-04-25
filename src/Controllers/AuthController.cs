using ArtPortfolio.Models;
using ArtPortfolio.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtPortfolio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ProjectUser> _userManager;
        private RoleManager<ProjectUserRole> _roleManager;
        private SignInManager<ProjectUser> _signInManager;
        public AuthController(UserManager<ProjectUser> userManager,
            RoleManager<ProjectUserRole> roleManager,
            SignInManager<ProjectUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }

        [HttpPost("passwordreset")]
        public async Task<IActionResult> PasswordReset(ResetPasswordDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
