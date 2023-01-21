using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace N_Chat.Server.Data
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //Följ detta sättet att namnge tables Testmodel = Test, ChatModel = Chat
        public DbSet<TestModel> Test { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);


            // Restrict deletion of thread on message delete (set user to null instead)
            modelBuilder.Entity<MessageModel>()
                .HasOne(i => i.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasNoKey();
        }

    }
}
