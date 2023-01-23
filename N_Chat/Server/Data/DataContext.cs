﻿using Microsoft.AspNetCore.Identity;
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
                .HasForeignKey(z => z.ChatId)
                .HasForeignKey(i => i.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasNoKey();
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasNoKey();
            var Hash = new PasswordHasher<UserModel>();
            var UserAdmin = new UserModel()
            {
                Id = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                Email = "Admin@Mail.com",
                UserName = "admin",
                NormalizedEmail = "ADMIN@MAIL.COM",
                NormalizedUserName = "admin",
                EmailConfirmed = true,
                PasswordHash = Hash.HashPassword(null!, "qwe123"),
            };
            var seedChat = new ChatModel()
            {
                Id = 1,
                Name = "CoolChat",
                CreatorId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                IsChatEncrypted = false,
                IsChatEnded = false,
                IsChatEdited = false,
                ChatCreated = DateTime.Now,
                ChatEnded = null,
                Messages = null,
                Users = null,
                UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",

            };

            var seedMessage = new MessageModel()
            {
                MessageId = 1,
                Message = "This is just a test message for the api's glhf",
                MessageCreated = DateTime.Now,
                MessageDeleted = null,
                MessageEdited = null,
                IsMessageEncrypted = false,
                IsMessageEdited = false,
                IsMessageDeleted = false,
                ChatId = 1,         
            };


            modelBuilder.Entity<UserModel>().HasData(UserAdmin);
            modelBuilder.Entity<MessageModel>().HasData(seedMessage);
            modelBuilder.Entity<ChatModel>().HasData(seedChat);
            
        }
        
    }
}
