using ArtPortfolio.Data;
using ArtPortfolio.Handlers.ProjectImageHandler;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.Hosting;
using ArtPortfolio.Models;

namespace ArtPortfolio.Tests.HandlerTests
{
    [Collection("PortfolioTests")]
    public class ProjectImageHandlerUnitTests
    {
        private IProjectImageHandler _sut;
        private ArtPortfolioDbContext _ctx;
        private Mock<IFormFile> _file;
        private Mock<IWebHostEnvironment> mockWebHost;

        public ProjectImageHandlerUnitTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);

            var basePath = Path.GetFullPath(@"C:\Users\kashe\Desktop");
            var testDir = Path.Combine(basePath, "testfolder");

            mockWebHost = new Mock<IWebHostEnvironment>();
            mockWebHost.SetupGet(w => w.ContentRootPath).Returns(testDir);

            _file = new Mock<IFormFile>();
            _file.SetupGet(f => f.FileName).Returns("FakeFile");
            _file.SetupGet(f => f.Length).Returns(81290);
            _file.SetupGet(f=> f.ContentType).Returns("image/jpeg");

            _sut = new ProjectImageHandler(mockWebHost.Object);
        }

        [Fact]
        public async Task SaveImage_Should_Create_ALocalDirectoryWithTheFile()
        {
            //arrange
            var initialFileName = "TestImage.png";
            var projectId = Guid.Parse("ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var mediaObject = await Utilities.GetProjectImageAsync(_ctx);
            mediaObject.ProjectId = projectId;

            //act
            var result = await _sut.SaveImage(mediaObject, initialFileName, _file.Object);

            //assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteImage_Should_Delete_TheSpecifiedFile()
        {
            //arrange
            var projectId = Guid.Parse("ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var mediaObject = await CreateTestFileToDelete(_ctx);
            mediaObject.ProjectId = projectId;

            //act
            var result = _sut.DeleteImage(_file.Object, mediaObject);

            //assert
            Assert.True(result);
        }




        private async Task<ProjectImage> CreateTestFileToDelete(ArtPortfolioDbContext _ctx) 
        {
            var testFile = await  Utilities.GetProjectImageAsync(_ctx);
            var basePath = Path.GetFullPath(@"C:\Users\kashe\Desktop");
            var testDir = Path.Combine(basePath, "testfolder", "ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var finalFileName = String.Format("{0}.png", testFile.Id);

            var testFileLocation = Path.Combine(testDir, finalFileName);

            if (!Directory.Exists(testDir))
            {
                Directory.CreateDirectory(testDir);
            }

            using (var fs = new FileStream(testFileLocation, FileMode.Create))
            {
                fs.CopyToAsync(fs);
            }

            return testFile;
        }
    }
}
