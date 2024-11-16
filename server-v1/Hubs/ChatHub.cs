using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Server_V1.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(ChatMessage message)
        {
            await Clients.All.SendAsync("send", message);
        }
    }

    [Serializable]
    public class ChatMessage
    {
        public DateTime Time { get; set; }
        public string Message { get; set; }
    }
}
