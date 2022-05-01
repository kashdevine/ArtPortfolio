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
    }
}
