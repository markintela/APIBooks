using Data.Context;
using Manager.Interfaces;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Manager.Implementation;
using Serilog;
using Manager.Mappings;
using Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebBooksAPI.Configurations;
using WebBooksAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

var chave = builder.Configuration.GetSection("JWT:Secret").Value;
var issuer = builder.Configuration.GetSection("JWT:Issuer").Value;
var audience = builder.Configuration.GetSection("JWT:Audience").Value;
var database = builder.Configuration.GetConnectionString("DBConnection");


// Middleware erros
builder.Services.AddTransient<ExceptionHadlingMiddleware>();

// Log Config
builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Async(log => log.Console())
    .WriteTo.Async(log => log.File("logs/log.txt", fileSizeLimitBytes:10000, rollOnFileSizeLimit:true,rollingInterval:RollingInterval.Day)));


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Auto Mapper Config 
builder.Services.AddAutoMapperConfiguration();

// Dependecy Injection
builder.Services.AddDependencyInjectionConfiguration();

// JWT Config
builder.Services.AddJWTConfigurationonfiguration(chave,issuer,audience);

// Database services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(database);
});

// Swagger Configs
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
// Exceeption Handling
app.UseMiddleware<ExceptionHadlingMiddleware>();

app.Run();
