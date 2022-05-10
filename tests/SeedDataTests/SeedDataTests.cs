using ArtPortfolio.Contracts;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using ArtPortfolio.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.SeedDataTests
{
    [Collection("PortfolioTests")]
    public class SeedDataTests
    {
        private ArtPortfolioUserDbContext _ctx;
        private ISeedData _sut;
        private Mock<UserManager<ProjectUser>> _mockUserManager;
        private Mock<IConfiguration> _mockConfig;

        public SeedDataTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioUserDbContext>()
                                                                        .UseSqlServer(connectionString: Utilities.UserConnectionString)
                                                                        .Options;
            var store = new Mock<IUserStore<ProjectUser>>();
            var mockAdmninUsername = new Mock<IConfigurationSection>();
            var mockAdmninPassword = new Mock<IConfigurationSection>();
            var mockAdmninEmail = new Mock<IConfigurationSection>();
            var mockAdmninFirstName = new Mock<IConfigurationSection>();
            var mockAdmninLastName = new Mock<IConfigurationSection>();
            


            mockAdmninUsername.Setup(x => x.Value).Returns("TestUserName");
            mockAdmninPassword.Setup(x => x.Value).Returns("Password12!@#");
            mockAdmninEmail.Setup(x => x.Value).Returns("testemail@gmail.com");
            mockAdmninFirstName.Setup(x => x.Value).Returns("FirstNameTest");
            mockAdmninLastName.Setup(x => x.Value).Returns("LasttNameMcTesterson");

            _mockUserManager = new Mock<UserManager<ProjectUser>>(store.Object, null, null, null, null, null, null, null, null);
            _mockConfig = new Mock<IConfiguration>();

            _mockConfig.Setup(c => c.GetSection(It.Is<string>(s => s == "AdminProf:Username"))).Returns(mockAdmninUsername.Object);
            _mockConfig.Setup(c => c.GetSection(It.Is<string>(s => s == "AdminProf:Password"))).Returns(mockAdmninPassword.Object);
            _mockConfig.Setup(c => c.GetSection(It.Is<string>(s => s == "AdminProf:Email"))).Returns(mockAdmninEmail.Object);
            _mockConfig.Setup(c => c.GetSection(It.Is<string>(s => s == "AdminProf:FirstName"))).Returns(mockAdmninFirstName.Object);
            _mockConfig.Setup(c => c.GetSection(It.Is<string>(s => s == "AdminProf:LastName"))).Returns(mockAdmninLastName.Object);

            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<ProjectUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _mockUserManager.Setup(u => u.AddClaimsAsync(It.IsAny<ProjectUser>(), It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(IdentityResult.Success);

            _ctx = new ArtPortfolioUserDbContext(dbContextOptions);
            _sut = new SeedData(_ctx, _mockUserManager.Object, _mockConfig.Object);
        }

        [Fact]
        public async Task SeedAdminUser_Should_Add_TheAdminUserToTheDb()
        {
            //arrange
            await Utilities.ReIntializeUserDb(_ctx);

            //act
            var result = await _sut.SeedAdminUser();

            //assert
            Assert.True(result);
        }

    }
}
