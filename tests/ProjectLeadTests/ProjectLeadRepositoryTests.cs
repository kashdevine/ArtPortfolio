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

namespace ArtPortfolio.Tests.ProjectLeadTests
{
    [Collection("PortfolioTests")]
    public class ProjectLeadRepositoryTests
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectLeadRepository _sut;
        public ProjectLeadRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectLeadRepository(_ctx);
        }


        [Fact]
        public async Task CreateLead_Should_ReturnTheCreatedLead()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = new ProjectLead() { Email = "newuseragain@gmail.com", FirstName = "NewlyCreatedUser", LastName = "NewlyCreateduserLastname", Message="TestMessage" };

            //act
            var result = await _sut.CreateLead(expected);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task Deletelead_Should_ReturnTrueIfLeadIsDeleted()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var deletedObj = await Utilities.GetProjectLeadAsync(_ctx);
            //act
            var result = await _sut.DeleteLead(deletedObj.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task GetAllLeads_Should_ReturnListOfLeads()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);

            //act
            var result = await _sut.GetAllLeads();
            //assert
            Assert.True(result.Count() >= 0);
        }

        [Fact]
        public async Task GetLeadById_Should_ReturnedSpecifiedLead()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var expected = await Utilities.GetProjectLeadAsync(_ctx);

            //act
            var result = await _sut.GetLeadById(expected.Id);

            //assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task UpdateLead_Should_ReturnUpdatedLead()
        {
            //arrange
            await Utilities.ReInitializeTestDb(_ctx);
            var updatedLead = await Utilities.GetProjectLeadAsync(_ctx);
            updatedLead.Email = "updatedemail@yahoo.com";

            //act
            var result = await _sut.UpdateLead(updatedLead);

            //assert
            Assert.Equal(updatedLead.Email, result.Email);
        }
    }
}
