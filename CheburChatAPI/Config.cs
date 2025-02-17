using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class Config
{
    public static void ServiceConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
            {
                RequireAudience = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,

            }
        );
        builder.Services.AddCors();
        builder.Services.AddSwaggerGen();
        builder.Services.AddEndpointsApiExplorer();
    }

    public static void AppConfig(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors(o => o
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}