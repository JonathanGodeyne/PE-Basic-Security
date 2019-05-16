using Microsoft.AspNetCore.Identity;
using System;
namespace Hybrid_Crypt_IRC.Data
{
    public class ChatCryptUser : IdentityUser
    {
        [PersonalData]
        public string CryptChatKeyStoragePath { get; set; }
    }
}