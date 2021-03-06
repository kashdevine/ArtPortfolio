using ArtPortfolio.Contracts;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IAuthHelpers"/>
    /// </summary>
    public class AuthHelpers : IAuthHelpers
    {
        private readonly string AccessJWT = "Accessjwt";
        private readonly string RefreshToken = "Refreshtoken";

        public void DeleteTokens(HttpResponse response)
        {
            response.Cookies.Delete(AccessJWT);
            response.Cookies.Delete(RefreshToken);
        }

        public async Task<IList<Claim>> GetUserClaimsAsync(string userName, UserManager<ProjectUser> userManager)
        {
            //Get the user to generate a token
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            // Get the user
            return await userManager.GetClaimsAsync(user);
        }

        public void SetTokens(string accessToken, string refreshToken, HttpResponse response)
        {
            // Append JWT and Refresh Token
            response.Cookies.Append(AccessJWT, accessToken);
            response.Cookies.Append(RefreshToken, refreshToken);
        }
    }
}
