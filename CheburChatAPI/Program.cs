using Domain.BusinessEntites.DTOs;
using Infrastructure.DataBase.Repos;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

MessageRepo messageRepo = new();
UserRepo userRepo = new();

userRepo.RegisterAsync(new("123", "123", "123"));
Guid userId = await userRepo.GetId("123");

ChatRepo chatRepo = new();


app.MapPost("chats", (CreateChatDTO dto) =>
{
    chatRepo.CreateAsync(dto, userId);
});

app.MapGet("chats/{id}", (Guid id) =>
{
    return chatRepo.ReadAsync(id, userId);
});

app.MapGet("users", () =>
{
    return userRepo.ReadAsync(userId);
});


app.Run();
