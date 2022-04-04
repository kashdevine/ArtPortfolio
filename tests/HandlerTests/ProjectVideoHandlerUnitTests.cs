using ArtPortfolio.Data;
using ArtPortfolio.Handlers.ProjectImageHandler;
using ArtPortfolio.Handlers.ProjectVideoHandlers;
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


            var basePath = Path.GetFullPath(Environment.SpecialFolder.CommonDesktopDirectory.ToString());
            var testDir = Path.Combine(basePath, "testfolder");

            mockWebHost = new Mock<IWebHostEnvironment>();
            mockWebHost.SetupGet(w => w.ContentRootPath).Returns(testDir);

            var mockConfigSection = new Mock<IConfigurationSection>();
            mockConfigSection.Setup(c => c.Value).Returns("ProjectImages");

            mockConfig = new Mock<IConfiguration>();
            mockConfig.Setup(m => m.GetSection(It.Is<string>(s => s == "ImageFileDir"))).Returns(mockConfigSection.Object);

            _file = new Mock<IFormFile>();
            _file.SetupGet(f => f.FileName).Returns("FakeVideo.mp4");
            _file.SetupGet(f => f.Length).Returns(81290);
            _file.SetupGet(f => f.ContentType).Returns("video/mp4");
            _file.SetupGet(f => f.Name).Returns("FakeVideo");

            _sut = new ProjectVideoHandler(mockWebHost.Object, mockConfig.Object);

        }
    }
}
