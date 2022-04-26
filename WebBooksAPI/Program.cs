using Data.Context;
using Manager.Interfaces;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Manager.Implementation;
using Serilog;
using Manager.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Log Config
builder.Host.UseSerilog((ctx, config) => config
    .MinimumLevel.Information()
    .WriteTo.Async(log => log.Console())
    .WriteTo.Async(log => log.File("logs/log.txt", fileSizeLimitBytes:10000, rollOnFileSizeLimit:true,rollingInterval:RollingInterval.Day)));


// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Dependecy Injection
builder.Services.AddAutoMapper(typeof(NewLibraryMappingProfile));
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILibraryManager, LibraryManager>();
builder.Services.AddScoped<IBookManager, BookManager>();


// Database services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

// Swagger Configs
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title="API Books-Library", Version="V1" });
    }
 );

var app = builder.Build();



app.UseExceptionHandler("/error");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
