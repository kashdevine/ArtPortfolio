using Serilog;
using Microsoft.OpenApi.Models;
using ArtPortfolio.Data;
using Microsoft.EntityFrameworkCore;
using ArtPortfolio.Contract;
using ArtPortfolio.Services;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
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

builder.Services.AddTransient<IProjectImageRepository, ProjectImageRepository>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
