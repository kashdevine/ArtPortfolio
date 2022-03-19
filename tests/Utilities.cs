﻿using ArtPortfolio.Data;
using ArtPortfolio.Models;
using Microsoft.EntityFrameworkCore;
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
            await ctx.ProjectVideos!.AddRangeAsync(SeedProjectVideos());
            await ctx.Projects!.AddRangeAsync(SeedProjects());

            await ctx.SaveChangesAsync();
        }

        public static async Task ReInitializeTestDb(ArtPortfolioDbContext ctx)
        {
            if (!await ctx.Database.CanConnectAsync()) {
                await ctx.Database.EnsureCreatedAsync();
            }

            ctx.Remove(ctx.ProjectImages);
            ctx.Remove(ctx.ProjectVideos);
            ctx.Remove(ctx.Projects);
            

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
        public static async Task<Project> GetProjectAsync(ArtPortfolioDbContext ctx, string ProjectName = "Project1")
        {
            await SeedDbAsync(ctx);
            return await ctx.Projects.FirstOrDefaultAsync(i => i.Name == ProjectName);
        }
    }
}