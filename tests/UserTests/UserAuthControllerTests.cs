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
    [Collection("PortfolioTests")]
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
                                                                        .UseSqlServer(connectionString: Utilities.UserConnectionString)
                                                                        .Options;
            var testClaimList = new List<Claim>();

            var store = new Mock<IUserStore<ProjectUser>>();
            var roleStore = new Mock<IRoleStore<ProjectUserRole>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ProjectUser>>();
            var mockResponse = new Mock<HttpResponse>();
            var mockSeedData = new Mock<ISeedData>();



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
            _mockUserManager.Setup(x => x.ChangePasswordAsync(It.IsAny<ProjectUser>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ProjectUser());

            _mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            _mockSignInManager.Setup(x => x.SignOutAsync());

            mockSeedData.Setup(s => s.SeedAdminUser()).ReturnsAsync(true);


            _ctx = new ArtPortfolioUserDbContext(dbContextOptions, mockSeedData.Object);
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

        [Fact]
        public async Task Logout_Should_Return_OkStatusAtLogout()
        {
            //arrange
            //act
            var result = await _sut.Logout();

            //assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ResetPassword_Should_Return_OkStatusAfterReset()
        {
            //arrange
            var resetPasswordDto = new ResetPasswordDTO() { UserId = "abcedfghijklmnop", Password = "Password123!", NewPassword = "NewPassword123!" };

            //act
            var result = await _sut.PasswordReset(resetPasswordDto);

            //assert
            Assert.IsType<OkResult>(result);
        }
    }
}
