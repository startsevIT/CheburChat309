using Domain.BusinessEntites.DTOs;
using Infrastructure.DataBase.Repos;
using Microsoft.AspNetCore.Authorization;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.ServiceConfig();

var app = builder.Build();
app.AppConfig();

MessageRepo messageRepo = new();
UserRepo userRepo = new();
ChatRepo chatRepo = new();

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

List<WebSocket> clients = [];
app.Map("/ws", async (HttpContext ctx) =>
{
    WebSocket websocket = await ctx.WebSockets.AcceptWebSocketAsync();

    clients.Add(websocket);

    byte[] buffer = new byte[1024 * 4];

    WebSocketReceiveResult result = await websocket.ReceiveAsync(new (buffer),CancellationToken.None);

    while (result.CloseStatus == null)
    {

        string receiveMessage = Encoding.UTF8.GetString(buffer[..result.Count]);

        byte[] sendMessage = Encoding.UTF8.GetBytes(receiveMessage);

        foreach (var client in clients)
            await client.SendAsync(new ArraySegment<byte>(sendMessage), result.MessageType, result.EndOfMessage, CancellationToken.None);

        result = await websocket.ReceiveAsync(new(buffer), CancellationToken.None);
    }
    await websocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

    clients.Remove(websocket);
});


app.Run();

static Guid GetId(HttpContext ctx)
{
    return new(ctx.User.Claims.FirstOrDefault(x => x.Type == "id").Value);
}