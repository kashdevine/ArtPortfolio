using ArtPortfolio.Data;
using ArtPortfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtPortfolio.Tests
{
    public static class Utilities
    {
        public static readonly string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=ArtPortfolioDbTest;Trusted_Connection=True;MultipleActiveResultSets=true";

        public static async Task SeedDbAsync(ArtPortfolioDbContext ctx)
        {
            await ctx.ProjectImages!.AddRangeAsync(SeedProjectImges());

            await ctx.SaveChangesAsync();
        }

        public static async Task ReInitializeTestDb(ArtPortfolioDbContext ctx)
        {
            if (!await ctx.Database.CanConnectAsync()) {
                await ctx.Database.EnsureCreatedAsync();
            }

            ctx.Remove(ctx.ProjectImages);

            await SeedDbAsync(ctx);

        }

        public static List<ProjectImage> SeedProjectImges()
        {
            return new List<ProjectImage>()
            {
                new ProjectImage() { Name = "Image1", AltTitle="Image1AltTitle", Description="Crazy Image 1"},
                new ProjectImage() { Name = "Image2", AltTitle="Image2AltTitle", Description="Crazy Image 2"},
                new ProjectImage() { Name = "Image3", AltTitle="Image3AltTitle", Description="Crazy Image 3"},
                new ProjectImage() { Name = "Image4", AltTitle="Image4AltTitle", Description="Crazy Image 4"},
            };
        }
    }
}
