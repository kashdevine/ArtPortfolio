using ArtPortfolio.Contracts;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;

namespace ArtPortfolio.Services
{
    public class UserService : IUserService
    {
        private UserManager<ProjectUser> _userManager;
        private RoleManager<ProjectUserRole> _roleManager;
        private SignInManager<ProjectUser> _signInManager;
        public UserService(UserManager<ProjectUser> userManager, 
            RoleManager<ProjectUserRole> roleManager,
            SignInManager<ProjectUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public Task<bool> DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Logout(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Register(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(Guid id, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
