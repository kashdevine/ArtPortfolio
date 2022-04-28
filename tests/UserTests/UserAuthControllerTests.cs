using ArtPortfolio.Contracts;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtPortfolio.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Linq.Expressions;
using System.Threading;

namespace ArtPortfolio.Tests.UserTests
{
    public class UserAuthControllerTests
    {
        private ArtPortfolioUserDbContext _ctx;
        private AuthController _sut;
        private Mock<UserManager<ProjectUser>> _mockUserManager;
        private Mock<RoleManager<ProjectUserRole>> _mockRoleManager;
        private Mock<SignInManager<ProjectUser>> _mockSignInManager;
        private Mock<IJWTService> _mockIJWTService;
        private Mock<IAuthHelpers> _mockAuthHelper;

        public UserAuthControllerTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioUserDbContext>()
                                                                        .UseSqlServer(connectionString: Utilities.ConnectionString)
                                                                        .Options;
            var testClaimList = new List<Claim>();

            var store = new Mock<IUserStore<ProjectUser>>();
            var roleStore = new Mock<IRoleStore<ProjectUserRole>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ProjectUser>>();
            var mockConfig = new Mock<IConfiguration>();
            var mockResponse = new Mock<HttpResponse>();

            _mockIJWTService = new Mock<IJWTService>();
            _mockAuthHelper = new Mock<IAuthHelpers>();
            _mockUserManager = new Mock<UserManager<ProjectUser>>(store.Object, null, null, null, null, null, null, null, null);
            _mockRoleManager = new Mock<RoleManager<ProjectUserRole>>(roleStore.Object, null, null, null, null);
            _mockSignInManager = new Mock<SignInManager<ProjectUser>>(_mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);

            _mockAuthHelper.Setup(x => x.GetUserClaimsAsync(It.IsAny<string>(), _mockUserManager.Object)).ReturnsAsync(testClaimList);
            _mockAuthHelper.Setup(x => x.SetTokens(It.IsAny<string>(), It.IsAny<string>(), mockResponse.Object));

            _mockIJWTService.Setup(x => x.GetAccessToken(It.IsAny<Claim[]>())).Returns("AccessToken");
            _mockIJWTService.Setup(x => x.GetRefreshtoken()).Returns("AccessToken");

            _mockUserManager.Setup(x => x.GetClaimsAsync(It.IsAny<ProjectUser>())).ReturnsAsync(testClaimList);

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);

            _ctx = new ArtPortfolioUserDbContext(dbContextOptions);
            _sut = new AuthController(_mockUserManager.Object,
                _mockRoleManager.Object, _mockSignInManager.Object,
                _mockIJWTService.Object, _mockAuthHelper.Object);
        }

        [Fact]
        public async Task Login_Should_Return_OKStatusAtLogin()
        {
            //arrange
            var loginDto = new LoginDTO() { Username = "logmein@gmail.com", Password = "Password123!" };

            //act
            var result = await _sut.Login(loginDto);

            //assert
            Assert.IsType<OkResult>(result);
        }
    }
}
