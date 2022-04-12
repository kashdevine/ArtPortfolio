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

        [Fact]
        public async Task DeleteProjectAsync_Should_Return_TrueIfEntityIsDeleted()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var target = await Utilities.GetProjectAsync(_ctx);

            //act
            var result = await _sut.DeleteProjectAsync(target.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetProjectAsync_Should_Return_SpecifiedProject()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectAsync(_ctx);
            //act
            var result = await _sut.GetProjectAsync(expected.Id);

            //assert
            Assert.Equal(expected,result);
        }

        [Fact]
        public async Task GetProjectsAsync_Should_Return_CollectionOfProjects()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            //act
            var result = await _sut.GetProjectsAsync();

            //assert
            Assert.Equal(result.Count(), 4);
        }

        [Fact]
        public async Task UpdateProject_Should_Return_UpdatedProject()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectAsync(_ctx);
            expected.Name = "This is updated now";

            //act
            var result = await _sut.UpdateProject(expected);

            //assert
            Assert.Equal(expected, result);
        }
    }
}
