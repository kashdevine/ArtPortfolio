using Serilog;
using Microsoft.OpenApi.Models;
using ArtPortfolio.Data;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();