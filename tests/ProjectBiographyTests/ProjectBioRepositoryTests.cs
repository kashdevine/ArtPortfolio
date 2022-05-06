using ArtPortfolio.Contract;
using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
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
    }
}
