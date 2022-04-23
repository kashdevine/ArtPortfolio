using ArtPortfolio.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ArtPortfolio.Controllers
{
    [Route("api/[controller]")]
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
    }
}
