using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;
using System;
using Hybrid_Crypt_IRC.Models;
namespace Hybrid_Crypt_IRC.Hubs
{
    public class CryptHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task EnterPassWord(string password)
        {
            await Clients.All.SendAsync("ReceivePassword", password );
        }
    }
}