using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="ISeedData"/>
    /// </summary>
    public class SeedData : ISeedData
    {
        private ArtPortfolioUserDbContext _ctx;
        private UserManager<ProjectUser> _userManager;
        private IConfiguration _config;

        public SeedData(ArtPortfolioUserDbContext ctx, UserManager<ProjectUser> userManager, IConfiguration config)
        {
            _ctx = ctx;
            _userManager = userManager;
            _config = config;
        }
        public async Task<bool> SeedAdminUser()
        {
            var user = new ProjectUser()
            {
                UserName = _config.GetValue<string>("AdminProf:Username"),
                NormalizedUserName = _config.GetValue<string>("AdminProf:Username").ToUpper(),
                Email = _config.GetValue<string>("AdminProf:Email"),
                NormalizedEmail = _config.GetValue<string>("AdminProf:Email").ToLower(),
                FirstName = _config.GetValue<string>("AdminProf:FirstName"),
                LastName = _config.GetValue<string>("AdminProf:LastName"),
                LockoutEnabled = false,
                EmailConfirmed = false,
                TwoFactorEnabled = false,
            };
            var passwordHasher = new PasswordHasher<ProjectUser>();
            var hashedPassword = passwordHasher.HashPassword(user, _config.GetValue<string>("AdminProf:Password"));
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, "Owner"),
                new Claim(ClaimTypes.Role, "Admin"),
            };
            var claimsAdded = await _userManager.AddClaimsAsync(user, claims);

            if (!claimsAdded.Succeeded)
            {
                throw new Exception("Could not add claims to the admin user");
            }

            user.PasswordHash = hashedPassword;

            var userExists = _ctx.Users.Any(x => x.UserName == user.UserName || x.Email == user.Email);

            if (userExists)
            {
                return false;
            }
            var userCreated = await _userManager.CreateAsync(user, user.PasswordHash);
            if (!userCreated.Succeeded)
            {
                throw new Exception("Could not create user.");
            }

            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
