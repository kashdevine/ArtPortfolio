using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using ArtPortfolio.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.ProjectTests
{
    [Collection("PortfolioTests")]
    public class ProjectRepositoryTests
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectRepository _sut;

        public ProjectRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectRepository(_ctx);
        }

        [Fact]
        public async Task CreateProject_Should_Return_TheCreatedProject()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = new Project() { Name = "This is the created project", Description = "I'm new and created", MainPhotoName = "some guid.png" };

            //act
            var result = await _sut.CreateProject(expected);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
