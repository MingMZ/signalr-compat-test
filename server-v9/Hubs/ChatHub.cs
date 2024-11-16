using Microsoft.AspNetCore.SignalR;

namespace Server_V9.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(ChatMessage message)
        {
            await Clients.All.SendAsync("send", message);
        }
    }

    [Serializable]
    public record ChatMessage
    {
        public required DateTime Time { get; init; }
        public required string Message { get; init; }
    }
}
