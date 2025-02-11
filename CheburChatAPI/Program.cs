using Domain.BusinessEntites.DTOs;
using Infrastructure.Auth;
using Infrastructure.DataBase.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder();

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

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(o => o
	.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();

MessageRepo messageRepo = new();
UserRepo userRepo = new();
ChatRepo chatRepo = new();

app.MapPost("/users/register", async (RegisterUserDTO dto) =>
{
	try
	{
		await userRepo.RegisterAsync(dto);
		return Results.Created();
	}
	catch (Exception ex)
	{
		return Results.Conflict(ex);
	}
});
app.MapPost("/users/login", async (LoginUserDTO dto) =>
{
    try
    {
        return Results.Ok(await userRepo.LoginAsync(dto));
    }
    catch (Exception ex)
    {
        return Results.Conflict(ex);
    }
});
app.MapGet("/users/account", [Authorize] async (HttpContext ctx) =>
{
    Guid id = GetId(ctx);
	try
	{
		return Results.Ok(await userRepo.ReadAsync(id));
	}
	catch (Exception ex)
	{
		return Results.BadRequest(ex.Message);
	}
});

app.Run();

static Guid GetId(HttpContext ctx)
{
    return new(ctx.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
}