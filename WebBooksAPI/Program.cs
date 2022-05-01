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
builder.Services.AddScoped<IUserAPIRepository, UserAPIRepository>();
builder.Services.AddScoped<ILibraryManager, LibraryManager>();
builder.Services.AddScoped<IBookManager, BookManager>();
builder.Services.AddScoped<IUserAPIManager, UserAPIManager>();


builder.Services.AddSingleton<IJWTService , JWTService>();
var chave = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value);
var issuer = builder.Configuration.GetSection("JWT:Issuer").Value;
var audience = builder.Configuration.GetSection("JWT:Audience").Value;

builder.Services.AddAuthentication(p =>
{
   p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer( p=>
    {
        p.RequireHttpsMetadata = false;
        p.SaveToken = true;
        p.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(chave),
            ValidateIssuer = true,
            ValidIssuer =  issuer,
            ValidAudience = audience,
            ValidateLifetime  = true
        };
    }   
);



// Database services
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

// Swagger Configs
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title="API Books-Library", Version="V1" });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insert Token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id ="Bearer"
                        }
                    },
                        Array.Empty<string>()
                    }
            });
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
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
