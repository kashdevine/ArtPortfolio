using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtPortfolio.Data
{
    public class ArtPortfolioDbContext : DbContext
    {
        public ArtPortfolioDbContext(DbContextOptions<ArtPortfolioDbContext> options) : base(options)
        {
        }

        public DbSet<ProjectImage>? ProjectImages { get; set; }
        public DbSet<ProjectVideo>? ProjectVideos { get; set; }
        public DbSet<ProjectLead>? ProjectLeads { get; set; }
        public DbSet<ProjectBiography>? ProjectBiographies { get; set; }
        public DbSet<Project>? Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany<ProjectVideo>()
                .WithOne();
            modelBuilder.Entity<Project>()
                .HasMany<ProjectImage>()
                .WithOne();

            modelBuilder.Entity<ProjectVideo>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(p=> p.ProjectId);

            modelBuilder.Entity<ProjectImage>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(p=> p.ProjectId);
        }
    }
}
