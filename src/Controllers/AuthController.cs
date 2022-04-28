using ArtPortfolio.Contracts;
using ArtPortfolio.Models;
using ArtPortfolio.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;

namespace ArtPortfolio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ProjectUser> _userManager;
        private RoleManager<ProjectUserRole> _roleManager;
        private SignInManager<ProjectUser> _signInManager;
        private IJWTService _jwt;
        private IAuthHelpers _authHelpers;
        private ILogger<AuthController> _logger;
        public AuthController(UserManager<ProjectUser> userManager,
            RoleManager<ProjectUserRole> roleManager,
            SignInManager<ProjectUser> signInManager,
            IJWTService jwt,
            IAuthHelpers authHelpers)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwt = jwt;
            _authHelpers = authHelpers;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            // Ensure the login information is correct
            var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, true, false);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var userClaims = await _authHelpers.GetUserClaimsAsync(login.Username, _userManager);

            // Generate token
            var jwtAccess = _jwt.GetAccessToken(userClaims.ToArray());
            var refreshToken = _jwt.GetRefreshtoken();

            _authHelpers.SetTokens(jwtAccess, refreshToken, Response);

            return Ok();
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
