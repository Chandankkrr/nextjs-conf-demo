using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace NextJSConf.Server.Hubs
{
    public static class ConnectedClients
    {
        public static List<string> ConnectedClientIds = new List<string>();
    }

    public class ChatHub : Hub
    {
        public async Task SendMessage(string x, string y, string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.ConnectionId, x, y, user, message);
        }

        public override async Task OnConnectedAsync()
        {
            ConnectedClients.ConnectedClientIds.Add(Context.ConnectionId);

            await Clients.All.SendAsync("UserConnected", Context.ConnectionId, ConnectedClients.ConnectedClientIds.Count);

            await Clients.All.SendAsync("LoadAllConnectedUsers", ConnectedClients.ConnectedClientIds);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedClients.ConnectedClientIds.Remove(Context.ConnectionId);

            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId, ConnectedClients.ConnectedClientIds.Count);

            //await Clients.All.SendAsync("LoadAllConnectedUsers", ConnectedClients.ConnectedClientIds);

            await base.OnDisconnectedAsync(exception);
        }
    }
}