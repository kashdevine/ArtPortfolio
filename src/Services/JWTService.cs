using ArtPortfolio.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret")));

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(creds);

            var payload = new JwtPayload(_config.GetValue<string>("JWT:Issuer"),
                                        _config.GetValue<string>("JWT:Audience"), 
                                        claims, 
                                        DateTime.UtcNow, 
                                        DateTime.Today.AddDays(_config.GetValue<int>("JWT:AccessTokenExpiration"))
                                        );
            var jwtToken = new JwtSecurityToken(jwtHeader, payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
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
