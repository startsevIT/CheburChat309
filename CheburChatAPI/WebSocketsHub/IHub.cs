using Domain.BusinessEntites.DTOs;

public interface IHub
{
    Task<CreateMessageDTO> AddSocketAndReceiveMessageAsync(WebSocketClient client);
    Task<CreateMessageDTO> RecieveMessageAsync(WebSocketClient client);
    Task RemoveSocket(WebSocketClient client);
    Task SendMessageAllAsync(WebSocketClient reciever, ReadMessageDTO dto);
}






