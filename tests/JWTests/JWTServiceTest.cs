using ArtPortfolio.Contracts;
using ArtPortfolio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;

namespace ArtPortfolio.Tests.JWTests
{
    public class JWTServiceTest
    {
        private IJWTService _sut;
        private Mock<IConfiguration> _mockConfig;
        public JWTServiceTest()
        {
            var mockJWTSecretSection = new Mock<IConfigurationSection>();
            mockJWTSecretSection.Setup(s => s.Value).Returns("This is a secrect");

            var mockJWTIssuerSection = new Mock<IConfigurationSection>();
            mockJWTIssuerSection.Setup(s => s.Value).Returns("artportfolio.art");

            var mockJWTAudienceSection = new Mock<IConfigurationSection>();
            mockJWTAudienceSection.Setup(s => s.Value).Returns("artportfolio.art");

            var mockJWTAccessTokenExpirationSection = new Mock<IConfigurationSection>();
            mockJWTAccessTokenExpirationSection.Setup(s => s.Value).Returns("5");

            var mockJWTRefreshTokenExpirationSection = new Mock<IConfigurationSection>();
            mockJWTRefreshTokenExpirationSection.Setup(s => s.Value).Returns("10");

            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(c => c.GetSection("JWT:Secret")).Returns(mockJWTSecretSection.Object);
            _mockConfig.Setup(c => c.GetSection("JWT:Issuer")).Returns(mockJWTIssuerSection.Object);
            _mockConfig.Setup(c => c.GetSection("JWT:Audience")).Returns(mockJWTAudienceSection.Object);
            _mockConfig.Setup(c => c.GetSection("JWT:AccessTokenExpiration")).Returns(mockJWTAccessTokenExpirationSection.Object);
            _mockConfig.Setup(c => c.GetSection("JWT:RefreshTokenExpiration")).Returns(mockJWTRefreshTokenExpirationSection.Object);



            _sut = new JWTService(_mockConfig.Object);
        }
    }
}
