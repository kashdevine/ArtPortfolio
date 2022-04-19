using Serilog;
using Microsoft.OpenApi.Models;
using ArtPortfolio.Data;
using Microsoft.EntityFrameworkCore;
using ArtPortfolio.Contract;
using ArtPortfolio.Services;
using Microsoft.Extensions.FileProviders;
using ArtPortfolio.Contracts;
using ArtPortfolio.Models;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Art Portfolio API",
        Description = "API For managing an art profolio backend."
    });
});

builder.Host.UseSerilog((ctx, logger) =>
{
    logger.Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Console();
});

builder.Services.AddDbContext<ArtPortfolioDbContext>(opt =>
{
    var connstring = builder.Configuration.GetConnectionString("ArtPortfolioDb");
    opt.UseSqlServer(connstring);
});

builder.Services.AddIdentityCore<ProjectUser>().AddRoles<ProjectUserRole>();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredUniqueChars = 1;
    opt.Password.RequireDigit = true;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;

    opt.Lockout.AllowedForNewUsers = true;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    opt.Lockout.MaxFailedAccessAttempts = 5;

    opt.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.Cookie.Name = builder.Configuration.GetValue<string>("CookieDefaultName");
    opt.ExpireTimeSpan = TimeSpan.FromMinutes(50);
    opt.Cookie.HttpOnly = true;
});

builder.Services.AddTransient<IProjectImageRepository, ProjectImageRepository>();
builder.Services.AddTransient<IProjectVideoRepository, ProjectVideoRepository>();
builder.Services.AddTransient<IProjectRepository, ProjectRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();


var ImageFileDir = builder.Configuration.GetValue<string>("ImageFileDir");
var VideoFileDir = builder.Configuration.GetValue<string>("VideoFileDir");

app.UseStaticFiles(new StaticFileOptions
{
    
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, ImageFileDir)),
    RequestPath = String.Format("\\{0}", ImageFileDir)
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, VideoFileDir)),
    RequestPath = String.Format("\\{0}", VideoFileDir)
});

app.UseCors(opt =>
    opt.AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .WithOrigins(new[] { "http://localhost:3000" })
);

app.UseAuthorization();

app.MapControllers();

app.Run();
