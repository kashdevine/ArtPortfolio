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
    }
}
