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

namespace ArtPortfolio.Tests.ProjectVideoTests
{
    [Collection("PortfolioTests")]
    public class ProjectVideoRepositoryTests
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectVideoRepository _sut;

        public ProjectVideoRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectVideoRepository(_ctx);
        }

        [Fact]
        public async Task CreateProjectVideoAsync_Should_Return_TheCreatedVideo()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = new ProjectVideo() { Name = "CreatedVideo", Description = "CreatedProjectTest", LinkURI = "google.com" };

            //act
            var result = await _sut.CreateProjectVideoAsync(expected);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task DeleteProjectVideoAsync_Should_Return_TrueIfVideoIsDeleted()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectVideoAsync(_ctx);

            //act
            var result = await _sut.DeleteProjectVideoAsync(expected.Id);

            //assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task GetProjectVideoAsync_Should_Return_SpecifiedVideo()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectVideoAsync(_ctx);

            //act
            var result = await _sut.GetProjectVideoAsync(expected.Id);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetProjectVideosAsync_Should_Return_CollectionOfVideos()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);

            //act
            var result = await _sut.GetProjectVideosAsync();

            //assert
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public async Task UpdateProjectVideoAsync_Should_Return_UpdatedVideo()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectVideoAsync(_ctx);
            expected.Name = "Now I am updated";

            //act
            var result = await _sut.UpdateProjectVideoAsync(expected);

            //assert
            Assert.Equal(expected, result);
        }
    }
}

