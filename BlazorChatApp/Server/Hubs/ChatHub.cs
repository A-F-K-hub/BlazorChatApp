using Microsoft.AspNetCore.SignalR;

namespace BlazorChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await AddMessageToChat("", "User Connected!");
            await base.OnConnectedAsync();
        }
        //Send msgs to all users and clients connected to hub
        public async Task AddMessageToChat(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
