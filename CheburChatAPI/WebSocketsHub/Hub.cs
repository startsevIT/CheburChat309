using Domain.BusinessEntites.DTOs;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

public class Hub : IHub
{
    private readonly List<WebSocketClient> clients = [];

    private readonly JsonSerializerOptions Options  = new() //Настройка для корректной сериализации Кириллицы
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true
    };

    public async Task<CreateMessageDTO> AddSocketAndReceiveMessageAsync(WebSocketClient client)
    {
        clients.Add(client);
        return await RecieveMessageAsync(client);
    }

    public async Task<CreateMessageDTO> RecieveMessageAsync(WebSocketClient client)
    {
        client.Result = await client.Socket.ReceiveAsync(new(client.Buffer), CancellationToken.None);
        try
        {
            return JsonSerializer.Deserialize<CreateMessageDTO>(Encoding.UTF8.GetString(client.Buffer[..client.Result.Count]));
        }
        catch (Exception ex)
        {
            throw new Exception("Некорректные данные");
        } 
    }

    public async Task RemoveSocket(WebSocketClient client)
    {
        if (!client.IsClose)
            throw new Exception("Соединение не разорвано");
        await client.Socket.CloseAsync(client.Result.CloseStatus.Value, client.Result.CloseStatusDescription, CancellationToken.None);
        clients.Remove(client);
    }

    public async Task SendMessageAllAsync(WebSocketClient reciever, ReadMessageDTO dto)
    {
        byte[] sendMessage = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(dto, Options));
        foreach (var client in clients)
            await client.Socket.SendAsync(new ArraySegment<byte>(sendMessage), reciever.Result.MessageType, reciever.Result.EndOfMessage, CancellationToken.None);
    }
}






