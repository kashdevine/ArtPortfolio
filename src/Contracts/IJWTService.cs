namespace ArtPortfolio.Contracts
{
    using System.Security.Claims;

    public interface IJWTService
    {
        public string GetAccessToken(Claim[] claims);
        public ClaimsPrincipal VerifyToken(string token);
        public string GetRefreshtoken();
    }
}
