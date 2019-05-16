using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using System.Threading;
using System.Threading.Tasks;
using System;
using Hybrid_Crypt_IRC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Hybrid_Crypt_IRC.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
namespace Hybrid_Crypt_IRC.Hubs
{
    [Authorize]
    public class CryptHub : Hub
    {
        private readonly UserManager<ChatCryptUser> _userManager;
        private readonly ChatCryptContext _chatCryptDbContext;

        public CryptHub(UserManager<ChatCryptUser> UserManager, ChatCryptContext chatCryptDbContext)
        {
            this._userManager = UserManager;
            this._chatCryptDbContext = chatCryptDbContext;
        }

        public async Task SendMessage(string user, string message)
        {

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task EnterPassWord(string password)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, password);
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            var userInfo = _chatCryptDbContext.UserInfo.First(u => u.UserId.Equals(user.Id));

            if (userInfo == null)
            {
                var newUserInfo = new UserInfo()
                {
                    UserId = user.Id,
                    ConnectionId = Context.ConnectionId
                };

                _chatCryptDbContext.UserInfo.Add(userInfo);
            }
            else
            {
                userInfo.ConnectionId = Context.ConnectionId;

            }
            _chatCryptDbContext.SaveChanges();
            await base.OnConnectedAsync();


        }
    }
}