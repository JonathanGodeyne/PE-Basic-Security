
namespace Hybrid_Crypt_IRC.Models
{
    public class Message : BaseEntity
    {
        public string MessageText { get; set; }
        public string SenderId { get; set; }
        public ChatGroup ChatGroup { get; set; }

    }
}
