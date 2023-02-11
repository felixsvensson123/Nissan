using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Policy;
using N_Chat.Shared;
using Newtonsoft.Json;

namespace N_Chat.Server.Data
{
    public class DataContext : IdentityDbContext<UserModel>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TestModel> Test { get; set; }
        public DbSet<ChatModel> Chats { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<Connections> Connections { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                    
            /*modelBuilder.Entity<UserModel>(entity =>
            {
                entity.HasMany(u => u.Chats)
                    .WithOne(uc => uc.User)
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(u => u.Messages)
                    .WithOne(m => m.User)
                    .HasForeignKey(m => m.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });*/

            modelBuilder.Entity<ChatModel>(entity =>
            {
                /*
                entity.HasMany(c => c.Users)
                    .WithOne(uc => uc.Chat)
                    .HasForeignKey(uc => uc.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
                    */

                entity.HasMany(c => c.Messages)
                    .WithOne(m => m.Chat)
                    .HasForeignKey(m => m.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.HasKey(uc => new { uc.UserId, uc.ChatId });

                entity.HasOne(uc => uc.User)
                    .WithMany(u => u.Chats)
                    .HasForeignKey(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(uc => uc.Chat)
                    .WithMany(c => c.Users)
                    .HasForeignKey(uc => uc.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
          
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(i => new { i.UserId, i.RoleId });;
            var member = new IdentityRole()
                {Name = "Member", NormalizedName = "MEMBER", Id = "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b" };
            var administrator = new IdentityRole()
                {Name = "Admin", NormalizedName = "admin", Id = "d1678ba6-7957-21a7-96b5-12b64c06bc25"};
            var hasher = new PasswordHasher<UserModel>();
            var adminSeed = new UserModel() {
                Id = "ded90182-7b04-41e0-aef6-8977a4d1c292",
                Email = "adminuser@gmail.com",
                UserName = "admin",
                NormalizedEmail = "adminuser@gmail.com",
                NormalizedUserName = "admin",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "qwe123"),};
            
            var chatAdminSeed = new UserModel() {
                Id = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                Email = "Css@live.se",
                UserName = "felix",
                NormalizedEmail = "css@live.se",
                NormalizedUserName = "felix",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "qwe123"),};
            
            modelBuilder.Entity<IdentityRole>().HasData(member, administrator);
            modelBuilder.Entity<UserModel>().HasData(adminSeed, chatAdminSeed);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {UserId = chatAdminSeed.Id, RoleId = administrator.Id});
        }
    }
}

  /*
            /*modelBuilder.Entity<UserModel>()
                .HasMany(u => u.Chats)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);#1#
            modelBuilder.Entity<UserChat>()
                .HasKey(ucm => new {ucm.UserId, ucm.ChatId});

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Chats)
                .HasForeignKey(uc => uc.UserId);

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.ChatId);
            
            modelBuilder.Entity<ChatModel>()
                .HasMany(c => c.Messages)
                .WithOne(uc => uc.Chat)
                .HasForeignKey(uc => uc.ChatId);
            
            modelBuilder.Entity<MessageModel>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId);
                */
