using Domain.BusinessEntites.DTOs;
using Infrastructure.DataBase.Repos;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder();
builder.ServiceConfig();

var app = builder.Build();
app.AppConfig();

MessageRepo messageRepo = new();
UserRepo userRepo = new();
ChatRepo chatRepo = new();
Hub hub = new();


app.MapPost("/users/register", async (RegisterUserDTO dto) =>
{
    await userRepo.RegisterAsync(dto);
    return Results.Created();
});
app.MapPost("/users/login", async (LoginUserDTO dto) =>
{

    return Results.Ok(await userRepo.LoginAsync(dto));

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

app.MapPost("/chats",[Authorize] async (CreateChatDTO dto, HttpContext ctx) =>
{
    Guid userId = GetId(ctx);
    try
    {
        await chatRepo.CreateAsync(dto, userId);
        return Results.Created();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});
app.MapGet("/chats/{chatId}", [Authorize] async (HttpContext ctx, Guid chatId) =>
{
    Guid userId = GetId(ctx);
    try
    {
        return Results.Ok(await chatRepo.ReadAndLinkAsync(chatId, userId));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});


app.Map("/ws/{chatId}", [Authorize] async (HttpContext ctx, Guid chatId) =>
{
    Guid userId = GetId(ctx);

    WebSocket websocket = await ctx.WebSockets.AcceptWebSocketAsync();

    WebSocketClient client = new(websocket);

    CreateMessageDTO recieveMessage = await hub.AddSocketAndReceiveMessageAsync(client);


    while (!client.IsClose)
    {
        Guid messageId = await messageRepo.CreateAndReturnIdAsync(recieveMessage, userId, chatId);

        ReadMessageDTO sendMessage = await messageRepo.ReadAsync(messageId);

        await hub.SendMessageAllAsync(client, sendMessage);

        recieveMessage = await hub.RecieveMessageAsync(client);
    }
    await hub.RemoveSocket(client);
});


app.Run();

static Guid GetId(HttpContext ctx)
{
    return new(ctx.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
}






