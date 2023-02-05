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
                .HasForeignKey(i => i.UserId) //.HasForeignKey i.ChatId
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

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

            var seedChat = new ChatModel() {
                Id = 5,
                Name = "CoolChat",
                CreatorId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                IsChatEncrypted = false,
                IsChatEnded = false,
                IsChatEdited = false,
                ChatCreated = DateTime.Now,
                ChatEnded = null,
                Messages = null,
                UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",};


            var seedMessage1 = new MessageModel() {
                Id = 2,
                Message = "This one admin message 1",
                UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", //admin qwe123
                MessageCreated = DateTime.Now,
                MessageDeleted = null,
                MessageEdited = null,
                IsMessageEncrypted = false,
                IsMessageEdited = false,
                IsMessageDeleted = false,
                ChatId = 5,};

            var seedMessage2 = new MessageModel() {
                Id = 3,
                Message = "This one admin message 2",
                UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35", //admin qwe123
                MessageCreated = DateTime.Now.AddHours(5),
                MessageDeleted = null,
                MessageEdited = null,
                IsMessageEncrypted = false,
                IsMessageEdited = false,
                IsMessageDeleted = false,
                ChatId = 5,};

            var seedMessage3 = new MessageModel() {
                Id = 4,
                Message = "This is felix message 1",
                UserId = "ded90182-7b04-41e0-aef6-8977a4d1c292", //felix qwe123
                ChatId = 5,
                MessageCreated = DateTime.Now,
                MessageDeleted = null,
                MessageEdited = null,
                IsMessageEncrypted = false,
                IsMessageEdited = false,
                IsMessageDeleted = false,};
            
            var seedMessage4 = new MessageModel() {
                Id = 5,
                Message = "This is just a test message for the api's glhf",
                UserId = "ded90182-7b04-41e0-aef6-8977a4d1c292", //felix qwe123
                ChatId = 5,
                MessageCreated = DateTime.Now,
                MessageDeleted = null,
                MessageEdited = null,
                IsMessageEncrypted = false,
                IsMessageEdited = false,
                IsMessageDeleted = false,};

            modelBuilder.Entity<IdentityRole>().HasData(member, administrator);
            modelBuilder.Entity<UserModel>().HasData(adminSeed, chatAdminSeed);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
                {UserId = chatAdminSeed.Id, RoleId = administrator.Id});
            modelBuilder.Entity<MessageModel>().HasData(seedMessage1, seedMessage2, seedMessage3, seedMessage4);
            modelBuilder.Entity<ChatModel>().HasData(seedChat);
        }
    }
}