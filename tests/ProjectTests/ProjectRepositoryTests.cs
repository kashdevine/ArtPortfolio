using ArtPortfolio.Contracts;
using ArtPortfolio.Data;
using ArtPortfolio.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPortfolio.Tests.ProjectTests
{
    public class ProjectRepositoryTests
    {
        private ArtPortfolioDbContext _ctx;
        private IProjectRepository _sut;

        public ProjectRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ArtPortfolioDbContext>()
                    .UseSqlServer(connectionString: Utilities.ConnectionString)
                    .Options;
            _ctx = new ArtPortfolioDbContext(dbContextOptions);
            _sut = new ProjectRepository(_ctx);
        }
    }
}
