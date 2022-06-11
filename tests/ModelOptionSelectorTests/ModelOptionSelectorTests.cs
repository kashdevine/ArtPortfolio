using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using ArtPortfolio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.ModelOptionSelectorTests
{
    [Collection("PortfolioTests")]
    public class ModelOptionSelectorTests
    {
        private ArtPortfolioDbContext _ctx;
        private Mock<ILogger<ProjectBiographyService>> _mockBioLogger;
        public ModelOptionSelectorTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;

            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _mockBioLogger = new Mock<ILogger<ProjectBiographyService>>();
        }

        [Fact]
        public async Task SelectAsync_Should_SetASelectedBiography()
        {
            // arrange
            await Utilities.ReInitializeTestDb(_ctx);
            IModelOptionSelector<ProjectBiography> _bioSut = new ProjectBiographyService(_ctx, _mockBioLogger.Object);
            var newSelected = await Utilities.GetProjectBiographyAsync(_ctx, "Test title 2");
            var selectedId = newSelected.Id;

            //act
            await _bioSut.SelectAsync(selectedId);
            //assert
            Assert.True(newSelected.Selected);
        }

        [Fact]
        public async Task SelectAsync_Should_UnsetAlreadySelectedBiography()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            IModelOptionSelector<ProjectBiography> _bioSut = new ProjectBiographyService(_ctx, _mockBioLogger.Object);
            var currentlySelected = await Utilities.GetProjectBiographyAsync(_ctx);
            var newSelected = await Utilities.GetProjectBiographyAsync(_ctx, "Test title 2");
            var selectedId = newSelected.Id;

            //act
            await _bioSut.SelectAsync(selectedId);

            //assert
            Assert.False(currentlySelected.Selected);
        }
    }
}
