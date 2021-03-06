using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace BlazorChatApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }


        private static Dictionary<string, string> Users = new Dictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            string username = Context.GetHttpContext().Request.Query["username"];
            Users.Add(Context.ConnectionId, username);
            await AddMessageToChat(string.Empty, $"{username} joined the chat!");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string username = Users.FirstOrDefault(u => u.Key == Context.ConnectionId).Value;
            await AddMessageToChat(string.Empty, $"{username} left!");
        }

        //Send msgs to all users and clients connected to hub
        public async Task AddMessageToChat(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
