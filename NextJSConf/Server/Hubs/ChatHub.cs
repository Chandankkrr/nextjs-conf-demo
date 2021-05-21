using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NextJSConf.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string x, string y, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, x, y, user, message);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}