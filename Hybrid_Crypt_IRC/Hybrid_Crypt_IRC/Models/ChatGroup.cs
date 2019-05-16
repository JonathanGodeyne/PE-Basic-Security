using System.Collections.Generic;

namespace Hybrid_Crypt_IRC.Models
{
    public class ChatGroup : BaseEntity
    {
        
        public string GroupName { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        public ICollection<UserInfo> UserInfo { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}