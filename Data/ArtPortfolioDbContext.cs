using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioDbContext : DbContext
    {
        public ArtPortfolioDbContext(DbContextOptions<ArtPortfolioDbContext> options) : base(options)
        {
        }
    }
}
