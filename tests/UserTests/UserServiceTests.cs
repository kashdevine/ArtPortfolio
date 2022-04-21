using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ArtPortfolio.Services;

namespace ArtPortfolio.Tests.UserTests
{
    public class UserServiceTests
    {
        private ArtPortfolioUserDbContext _ctx;
        private IUserService _sut;
        private Mock<UserManager<ProjectUser>> _mockUserManager;
        private Mock<RoleManager<ProjectUserRole>> _mockRoleManager;

        public UserServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioUserDbContext>()
                     .UseSqlServer(connectionString: Utilities.ConnectionString)
                     .Options;
            
            _mockUserManager = new Mock<UserManager<ProjectUser>>();
            _mockRoleManager = new Mock<RoleManager<ProjectUserRole>>();

            _ctx = new ArtPortfolioUserDbContext(dbContextOptions);
            _sut = new UserService(_mockUserManager.Object, _mockRoleManager.Object);
        }
    }
}
