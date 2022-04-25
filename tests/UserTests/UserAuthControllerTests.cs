﻿using ArtPortfolio.Contracts;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPortfolio.Tests.UserTests
{
    public class UserAuthControllerTests
    {
        private ArtPortfolioUserDbContext _ctx;
        private AuthController _sut;
        private Mock<UserManager<ProjectUser>> _mockUserManager;
        private Mock<RoleManager<ProjectUserRole>> _mockRoleManager;
        private Mock<SignInManager<ProjectUser>> _mockSignInManager;

        public UserAuthControllerTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioUserDbContext>()
         .UseSqlServer(connectionString: Utilities.ConnectionString)
         .Options;

            _mockUserManager = new Mock<UserManager<ProjectUser>>();
            _mockRoleManager = new Mock<RoleManager<ProjectUserRole>>();
            _mockSignInManager = new Mock<SignInManager<ProjectUser>>();

            _ctx = new ArtPortfolioUserDbContext(dbContextOptions);
            _sut = new AuthController(_mockUserManager.Object, _mockRoleManager.Object, _mockSignInManager.Object);
        }
    }
}