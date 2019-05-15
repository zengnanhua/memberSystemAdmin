using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace AdminSystem.Application.Hubs
{

    [Authorize]
    public class NotificationsHub : Hub
    {
        [AllowAnonymous]
        public async Task<string> singleLogin()
        {
            await Clients.OthersInGroup(this.Context.User.FindFirst("UserName").Value).SendAsync("singleOutLogin");
            return "成功";
        }

        public async Task<string> SendMessage(string message)
        {
            IHubContext<NotificationsHub> dfd;
        
            await Clients.All.SendAsync("ReceiveMessage", message+"dfasdf");
            return "我发送成功了";
        }
        public async Task<string> SendMessage1(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage1", message + "dfasdf");
            return "我发送成功了";
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, this.Context.User.FindFirst("UserName").Value);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, this.Context.User.FindFirst("UserName").Value);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
