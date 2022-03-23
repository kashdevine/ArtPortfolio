using ArtPortfolio.Contract;
using ArtPortfolio.Data;
using ArtPortfolio.Models;
using ArtPortfolio.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.ProjectImageTests
{
    [Collection("PortfolioTests")]
    public class ProjectImageRepositoryTest
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectImageRepository _sut;
         

       public ProjectImageRepositoryTest()
       {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                                .UseSqlServer(connectionString:Utilities.ConnectionString)
                                .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectImageRepository(_ctx);
        }

        [Fact]
        public async Task CreateProjectImage_Should_Return_CreatedProjectImage()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = new ProjectImage() { Name = "CreatedImage", AltTitle = "AltCreatedImage", Description = "CreatedImage Description" };

            //act
            var result = await _sut.CreateProjectImage(expected);
            //assert
            Assert.Equal(expected, result);

        }
        
        [Fact]
        public async Task DeleteImageAsync_Should_Return_True_IfImageIsDeleted()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var deletedObject = await Utilities.GetProjectImageAsync(_ctx);
            //act
            var result = await _sut.DeleteImageAsync(deletedObject.Id);
            //assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task GetImageAsync_Should_Return_ImageIfItExists()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectImageAsync(_ctx);
            //act
            var result = await _sut.GetImageAsync(expected.Id);
            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetImagesAsync_Should_Return_ListOfImages()
        {
            //arramge
            await Utilities.ReInitializeTestDb(_ctx);
            //act
            var result = await _sut.GetImagesAsync();
            //assert
            Assert.True(result.Count() > 0);
        }
    }
}
