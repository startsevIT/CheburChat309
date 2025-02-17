using Domain.BusinessEntites.DTOs;
using Infrastructure.DataBase.Repos;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder();
builder.ServiceConfig();

var app = builder.Build();
app.AppConfig();

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