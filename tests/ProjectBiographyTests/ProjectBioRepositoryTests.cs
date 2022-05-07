using ArtPortfolio.Contract;
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

namespace ArtPortfolio.Tests.ProjectBiographyTests
{
    [Collection("PortfolioTests")]
    public class ProjectBioRepositoryTests
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectBioRepository _sut;

        public ProjectBioRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectBioRepository(_ctx);
        }

        [Fact]
        public async Task CreateBio_Should_ReturnNewlyCreatedBio()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var newObj = new ProjectBiography()
            {
                Body = "This is the whole project biography look it",
                Title = "New Bio"
            };
            //act
            var result = await _sut.CreateBio(newObj);

            //assert
            Assert.Equal(newObj, result);
        }
    }
}
