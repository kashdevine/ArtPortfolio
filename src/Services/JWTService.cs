using ArtPortfolio.Contracts;
using System.Security.Claims;

namespace ArtPortfolio.Services
{
    /// <summary>
    /// <inheritdoc cref="IJWTService"/>
    /// </summary>
    public class JWTService : IJWTService
    {
        private IConfiguration _config;
        public JWTService(IConfiguration config)
        {
            _config = config;
        }
        public string GetAccessToken(Claim[] claims)
        {
            throw new NotImplementedException();
        }

        public string GetRefreshtoken()
        {
            throw new NotImplementedException();
        }

        public ClaimsPrincipal VerifyAccessToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool VerifyRefeshToken(string refeshToken)
        {
            throw new NotImplementedException();
        }
    }
}
