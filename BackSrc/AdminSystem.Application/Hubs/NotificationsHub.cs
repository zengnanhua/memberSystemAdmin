using AdminSystem.Application.Services;
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
        IIdentityService _identityService;
        public NotificationsHub(IIdentityService identityService)
        {
            this._identityService = identityService;
        }

        public async Task<string> SendMessage(string message)
        {
            //await this.Groups.AddToGroupAsync(this.Context.ConnectionId, "dfa");
          
            await Clients.All.SendAsync("ReceiveMessage", message+"dfasdf");
            return "我发送成功了";
        }
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, _identityService.GetUserName());
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, _identityService.GetUserName());
            await base.OnDisconnectedAsync(ex);
        }
    }
}
