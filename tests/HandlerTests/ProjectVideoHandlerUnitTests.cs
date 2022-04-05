using ArtPortfolio.Data;
using ArtPortfolio.Handlers.ProjectImageHandler;
using ArtPortfolio.Handlers.ProjectVideoHandlers;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArtPortfolio.Tests.HandlerTests
{
    [Collection("PortfolioTests")]
    public class ProjectVideoHandlerUnitTests
    {
        private IProjectVideoHandler _sut;
        private ArtPortfolioDbContext _ctx;
        private Mock<IFormFile> _file;
        private Mock<IWebHostEnvironment> mockWebHost;
        private Mock<IConfiguration> mockConfig;

        public ProjectVideoHandlerUnitTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                                   .UseSqlServer(connectionString: Utilities.ConnectionString)
                                   .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);


            var basePath = Path.GetFullPath(@"C:\Users\kashe\Desktop");
            var testDir = Path.Combine(basePath, "testfolder");

            mockWebHost = new Mock<IWebHostEnvironment>();
            mockWebHost.SetupGet(w => w.ContentRootPath).Returns(testDir);

            var mockConfigSection = new Mock<IConfigurationSection>();
            mockConfigSection.Setup(c => c.Value).Returns("ProjectVideos");

            mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(m => m.GetSection(It.Is<string>(s => s == "VideoFileDir"))).Returns(mockConfigSection.Object);

            _file = new Mock<IFormFile>();
            _file.SetupGet(f => f.FileName).Returns("FakeVideo.mp4");
            _file.SetupGet(f => f.Length).Returns(81290);
            _file.SetupGet(f => f.ContentType).Returns("video/mp4");
            _file.SetupGet(f => f.Name).Returns("FakeVideo");

            _sut = new ProjectVideoHandler(mockWebHost.Object, mockConfig.Object);

        }
        
        [Fact]
        public async Task SaveVideo_Should_SaveVideo()
        {
            //arrange
            var projectId = Guid.Parse("ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var mediaObject = await Utilities.GetProjectVideoAsync(_ctx);
            mediaObject.ProjectId = projectId;
            //act
            var result = _sut.SaveVideo(_file.Object, mediaObject);

            //assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task DeleteVideo_Should_DeleteVideoIfItExists()
        {
            //arrange
            var projectId = Guid.Parse("ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var mediaObject = await CreateTestFileToDelete(_ctx);
            mediaObject.ProjectId = projectId;
            //act
            var result = _sut.DeleteVideo(mediaObject);

            //assert
            Assert.True(result);
        }

        private async Task<ProjectVideo> CreateTestFileToDelete(ArtPortfolioDbContext _ctx)
        {
            var testFile = await Utilities.GetProjectVideoAsync(_ctx);
            var basePath = Path.GetFullPath(@"C:\Users\kashe\Desktop");
            var testDir = Path.Combine(basePath, "testfolder", "ProjectVideos", "ae54138a-2b33-4704-bc9b-d5cb51b9574b");

            var finalFileName = String.Format("{0}.mp4", testFile.Id);

            var testFileLocation = Path.Combine(testDir, finalFileName);

            if (!Directory.Exists(testDir))
            {
                Directory.CreateDirectory(testDir);
            }

            using (var fs = new FileStream(testFileLocation, FileMode.Create))
            {
                fs.CopyToAsync(fs);
                testFile.FullFilePath = testFileLocation;
            }

            return testFile;

        }
    }
}
