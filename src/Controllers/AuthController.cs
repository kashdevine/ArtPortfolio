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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<ProjectUser> _userManager;
        private RoleManager<ProjectUserRole> _roleManager;
        private SignInManager<ProjectUser> _signInManager;
        private IJWTService _jwt;
        private IAuthHelpers _authHelpers;
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

        /// <summary>
        /// Login a user using username and password
        /// </summary>
        /// <param name="login">A Login DTO object</param>
        /// <returns>200 status on successful login</returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Logs out the user. 
        /// </summary>
        /// <returns>200 status if logout was successful.</returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _authHelpers.DeleteTokens(Response);

            return Ok();
        }

        /// <summary>
        /// Resets the password for the user.
        /// </summary>
        /// <param name="dto">A Resetpassword DTO</param>
        /// <returns>200 status if the reset is successful.</returns>
        [HttpPost("passwordreset")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PasswordReset(ResetPasswordDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            var result = await _userManager.ChangePasswordAsync(user, dto.Password, dto.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
