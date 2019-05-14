using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminSystem.Application.Hubs
{

    [Authorize]
    public class NotificationsHub : Hub
    {
        public async Task<string> SendMessage(string message)
        {
            //await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "dfa");
          
            await Clients.All.SendAsync("ReceiveMessage", message+"dfasdf");
            return "我发送成功了";
        }
    }
}
