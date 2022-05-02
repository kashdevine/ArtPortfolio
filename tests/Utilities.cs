using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            await ctx.ProjectVideos!.AddRangeAsync(SeedProjectVideos());
            await ctx.Projects!.AddRangeAsync(SeedProjects());
            await ctx.ProjectLeads.AddRangeAsync(SeedLeads());

            await ctx.SaveChangesAsync();
        }

        public static async Task ReInitializeTestDb(ArtPortfolioDbContext ctx)
        {
            if (!await ctx.Database.CanConnectAsync()) {
                await ctx.Database.EnsureCreatedAsync();
            }

            ctx.ProjectImages.RemoveRange(ctx.ProjectImages);
            ctx.ProjectVideos.RemoveRange(ctx.ProjectVideos);
            ctx.Projects.RemoveRange(ctx.Projects);
            ctx.ProjectLeads.RemoveRange(ctx.ProjectLeads);
            

            await SeedDbAsync(ctx);

        }

        public static List<ProjectImage> SeedProjectImges()
        {
            return new List<ProjectImage>()
            {
                new ProjectImage() { Name = "Image1", AltTitle="Image1AltTitle", Description="Crazy Image 1"},
                new ProjectImage() { Name = "Image2", AltTitle="Image2AltTitle", Description="Crazy Image 2"},
                new ProjectImage() { Name = "Image3", AltTitle="Image3AltTitle", Description="Crazy Image 3"},
                new ProjectImage() { Name = "Image4", AltTitle="Image4AltTitle", Description="Crazy Image 4"}
            };
        }

        public static List<ProjectVideo> SeedProjectVideos()
        {
            return new List<ProjectVideo>()
            {
                new ProjectVideo() { Name ="Vid1", Description="Crazy vid 1", LinkURI="google.com"},
                new ProjectVideo() { Name ="Vid2", Description="Crazy vid 2", LinkURI="bing.com"},
                new ProjectVideo() { Name ="Vid3", Description="Crazy vid 3", LinkURI="askjeeves.com"},
                new ProjectVideo() { Name ="Vid4", Description="Crazy vid 4", LinkURI="yahoo.com"}
            };
        }

        public static List<ProjectLead> SeedLeads()
        {
            return new List<ProjectLead>()
            {
               new ProjectLead() { FirstName = "LeadFirst1", LastName = "LeadSecond1", Message = "Message1", Email = "lead1@gmail.com", IsSpam = false},
               new ProjectLead() { FirstName = "LeadFirst2", LastName = "LeadSecond2", Message = "Message2", Email = "lead2@gmail.com", IsSpam = false},
               new ProjectLead() { FirstName = "LeadFirst3", LastName = "LeadSecond3", Message = "Message3", Email = "lead3@gmail.com", IsSpam = false},
               new ProjectLead() { FirstName = "LeadFirst4", LastName = "LeadSecond4", Message = "Message4", Email = "lead4@gmail.com", IsSpam = false},
            };
        }

        public static List<Project> SeedProjects()
        {
            return new List<Project>()
            {
                new Project() { Name ="Project1", MainPhotoName="ProjectMainImage1", Description="Crazy Project 1"},
                new Project() { Name ="Project2", MainPhotoName="ProjectMainImage2", Description="Crazy Project 2"},
                new Project() { Name ="Project3", MainPhotoName="ProjectMainImage3", Description="Crazy Project 3"},
                new Project() { Name ="Project4", MainPhotoName="ProjectMainImage4", Description="Crazy Project 4"}
            };
        }


        public static async Task<ProjectImage> GetProjectImageAsync(ArtPortfolioDbContext ctx, string ProjectImageName = "Image1")
        {
            await SeedDbAsync(ctx);
            return await ctx.ProjectImages.FirstOrDefaultAsync(i => i.Name == ProjectImageName);
        }
        public static async Task<ProjectVideo> GetProjectVideoAsync(ArtPortfolioDbContext ctx, string ProjectVideoName = "Vid1")
        {
            await SeedDbAsync(ctx);
            return await ctx.ProjectVideos.FirstOrDefaultAsync(v=> v.Name == ProjectVideoName);
        }
        public static async Task<ProjectLead> GetProjectLeadAsync(ArtPortfolioDbContext ctx, string ProjectLeadEmail = "lead1@gmail.com")
        {
            await SeedDbAsync(ctx);
            return await ctx.ProjectLeads.FirstOrDefaultAsync(v => v.Email == ProjectLeadEmail);
        }
        public static async Task<Project> GetProjectAsync(ArtPortfolioDbContext ctx, string ProjectName = "Project1")
        {
            await SeedDbAsync(ctx);
            return await ctx.Projects.FirstOrDefaultAsync(i => i.Name == ProjectName);
        }

        public static string GetRefreshToken()
        {
            var refreshClaims = new[] { new Claim(ClaimTypes.Role, "RefreshToken") };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a secrect"));

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(creds);

            var payload = new JwtPayload("artportfolio.art",
                                        "artportfolio.art",
                                        refreshClaims,
                                        DateTime.UtcNow,
                                        DateTime.Today.AddDays(10)
                                        );
            var refreshToken = new JwtSecurityToken(jwtHeader, payload);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken);
        }

        public static string GetAccessToken()
        {
            var claims = new[] {
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim("test", "TestClaim")
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is a secrect"));

            var creds = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtHeader = new JwtHeader(creds);

            var payload = new JwtPayload("artportfolio.art",
                                        "artportfolio.art",
                                        claims,
                                        DateTime.UtcNow,
                                        DateTime.Today.AddDays(5)
                                        );
            var jwtToken = new JwtSecurityToken(jwtHeader, payload);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
