using ArtPortfolio.Contracts;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioUserDbContext : IdentityDbContext
    {
        private ISeedData _seedData;
        public ArtPortfolioUserDbContext(DbContextOptions<ArtPortfolioUserDbContext> options, ISeedData seedData) : base(options)
        {
            _seedData = seedData;
        }

        public DbSet<ProjectUser> Users { get; set; }
        public DbSet<ProjectUserRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            _seedData.SeedAdminUser();
        }
    }
}
