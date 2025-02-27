using System.Net.WebSockets;

public class WebSocketClient(WebSocket socket)
{
    public WebSocket Socket { get; set; } = socket;
    public byte[] Buffer { get; set; } = new byte[1024 * 4];
    public WebSocketReceiveResult Result { get; set; }
    public bool IsClose { get => Result.CloseStatus != null; }
}






