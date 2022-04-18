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
            var refreshClaims = new[] { new Claim(ClaimTypes.Role, "RefreshToken") };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:Secret")));

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(creds);

            var payload = new JwtPayload(_config.GetValue<string>("JWT:Issuer"),
                                        _config.GetValue<string>("JWT:Audience"),
                                        refreshClaims,
                                        DateTime.UtcNow,
                                        DateTime.Today.AddDays(_config.GetValue<int>("JWT:RefreshTokenExpiration"))
                                        );
            var refreshToken = new JwtSecurityToken(jwtHeader, payload);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken);

        }

        public IEnumerable<Claim> VerifyAccessToken(string token)
        {
            var jwtToken = GetToken(token);

            return jwtToken == null ? Enumerable.Empty<Claim>() : jwtToken.Claims;
        }

        public bool VerifyRefeshToken(string refeshToken)
        {
            var token = GetToken(refeshToken);
            var refreshClaim = token.Claims.First(c => c.Value == "RefreshToken");

            if (refreshClaim == null || token.ValidTo < DateTime.Today)
            {
                return false;
            }

            return true;
            
        }

        private JwtSecurityToken GetToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("JWT:Secret"));
            var tokenValParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = _config.GetValue<string>("JWT:Issuer"),
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = _config.GetValue<string>("JWT:Audience"),
                ValidateAudience = true,
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValParameters, out SecurityToken finalToken);

                return (JwtSecurityToken) finalToken;

            }
            catch (Exception e)
            {
                throw new Exception(String.Format("Could not verify the token at {0}", nameof(VerifyRefeshToken)),e);
            }

        }
    }
}
