using Microsoft.EntityFrameworkCore;
using Hybrid_Crypt_IRC.Models;

namespace Hybrid_Crypt_IRC.Data

{
    public class ChatCryptContext : DbContext
    {
        public ChatCryptContext (DbContextOptions<ChatCryptContext> options):base(options){

        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<ChatGroup> GroupChat { get; set; }
    }
}