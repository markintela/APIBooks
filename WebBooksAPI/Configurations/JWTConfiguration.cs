using Data.Services;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebBooksAPI.Configurations
{
    public static class JWTConfiguration
    {
       
        public static void AddJWTConfigurationonfiguration(this IServiceCollection services, string chave, string issuer, string audience) {
         

            services.AddSingleton<IJWTService, JWTService>();
            
            services.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(p =>
            {
                p.RequireHttpsMetadata = false;
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(chave)),
                    ValidateIssuer = true,
                    ValidIssuer =  issuer,
                    ValidAudience = audience,
                    ValidateLifetime  = true
                };
            }
            );

        }
    }
}
