using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioUserDbContext : IdentityDbContext
    {
        public ArtPortfolioUserDbContext(DbContextOptions<ArtPortfolioUserDbContext> options) : base(options)
        {
        }

        public DbSet<ProjectUser> Users { get; set; }
        public DbSet<ProjectUserRole> Roles { get; set; }

    }
}
