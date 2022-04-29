using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ArtPortfolio.Contracts
{
    public interface IAuthHelpers
    {
        /// <summary>
        /// Returns a list of the user claims
        /// </summary>
        /// <param name="userName">A string.</param>
        /// <param name="userManager">A User Manager for the Project User.</param>
        /// <returns>A list of claims.</returns>
        public Task<IList<Claim>> GetUserClaimsAsync(string userName, UserManager<ProjectUser> userManager);

        /// <summary>
        /// Sets the cookies for the access and refresh tokens.
        /// </summary>
        /// <param name="accessToken">A JWT Token</param>
        /// <param name="refreshToken">A refresh JWT token.</param>
        /// <param name="response">HttpResponse Object from the controller.</param>
        public void SetTokens(string accessToken, string refreshToken, HttpResponse response);

        /// <summary>
        /// Deletes the access token and refresh token.
        /// </summary>
        /// <param name="response">HttpResponse Object from the controller.</param>
        public void DeleteTokens(HttpResponse response);
    }
}
