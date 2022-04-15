namespace ArtPortfolio.Contracts
{
    using System.Security.Claims;

    public interface IJWTService
    {
        /// <summary>
        /// Returns a new access token with the user claims.
        /// </summary>
        /// <param name="claims">System Security Claims</param>
        /// <returns>JWT Token</returns>
        public string GetAccessToken(Claim[] claims);
        /// <summary>
        /// Creates refresh token.
        /// </summary>
        /// <returns>JWT Refresh Token</returns>
        public string GetRefreshtoken();
        /// <summary>
        /// Verifies the refresh token isn't expired.
        /// </summary>
        /// <param name="refeshToken">A Refresh Token</param>
        /// <returns>A boolean.</returns>
        public bool VerifyRefeshToken(string refeshToken);
        /// <summary>
        /// Returns a claims principal from a verified JWT token.
        /// </summary>
        /// <param name="token">JWT Token</param>
        /// <returns>Claim Principal</returns>
        public ClaimsPrincipal VerifyAccessToken(string token);
    }
}
