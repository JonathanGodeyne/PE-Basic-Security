namespace Hybrid_Crypt_IRC.Models
{
    public class UserInfo : BaseEntity
    {
        public string UserId { get; set; }
        public string PublicKey { get; set; }

        public string ConnectionId { get; set; }
    }


}