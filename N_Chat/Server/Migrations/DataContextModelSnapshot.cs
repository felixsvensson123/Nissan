﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using N_Chat.Server.Data;

#nullable disable

namespace NChat.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = "e02d359e-6bfb-47ed-9fbc-4c99e5d2db9b",
                            ConcurrencyStamp = "45f60bf3-6610-4769-aa14-db8d69a60376",
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        },
                        new
                        {
                            Id = "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                            ConcurrencyStamp = "521bef94-a782-41ba-895b-10959b9f1172",
                            Name = "Admin",
                            NormalizedName = "admin"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            RoleId = "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("N_Chat.Shared.ChatModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChatCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ChatEnded")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsChatEdited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChatEncrypted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsChatEnded")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Chats");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            ChatCreated = new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(884),
                            CreatorId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                            IsChatEdited = false,
                            IsChatEncrypted = false,
                            IsChatEnded = false,
                            Name = "CoolChat",
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35"
                        });
                });

            modelBuilder.Entity("N_Chat.Shared.Connections", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Connected")
                        .HasColumnType("bit");

                    b.Property<string>("UserAgent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Connections");
                });

            modelBuilder.Entity("N_Chat.Shared.MessageModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<bool>("IsMessageDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMessageEdited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMessageEncrypted")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MessageCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MessageDeleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("MessageEdited")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            ChatId = 5,
                            IsMessageDeleted = false,
                            IsMessageEdited = false,
                            IsMessageEncrypted = false,
                            Message = "This one admin message 1",
                            MessageCreated = new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(915),
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35"
                        },
                        new
                        {
                            Id = 3,
                            ChatId = 5,
                            IsMessageDeleted = false,
                            IsMessageEdited = false,
                            IsMessageEncrypted = false,
                            Message = "This one admin message 2",
                            MessageCreated = new DateTime(2023, 2, 4, 23, 34, 28, 289, DateTimeKind.Local).AddTicks(917),
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35"
                        },
                        new
                        {
                            Id = 4,
                            ChatId = 5,
                            IsMessageDeleted = false,
                            IsMessageEdited = false,
                            IsMessageEncrypted = false,
                            Message = "This is felix message 1",
                            MessageCreated = new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(920),
                            UserId = "ded90182-7b04-41e0-aef6-8977a4d1c292"
                        },
                        new
                        {
                            Id = 5,
                            ChatId = 5,
                            IsMessageDeleted = false,
                            IsMessageEdited = false,
                            IsMessageEncrypted = false,
                            Message = "This is just a test message for the api's glhf",
                            MessageCreated = new DateTime(2023, 2, 4, 18, 34, 28, 289, DateTimeKind.Local).AddTicks(921),
                            UserId = "ded90182-7b04-41e0-aef6-8977a4d1c292"
                        });
                });

            modelBuilder.Entity("N_Chat.Shared.TestModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("N_Chat.Shared.UserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("ChatModelId")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatModelId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "ded90182-7b04-41e0-aef6-8977a4d1c292",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f92a9244-e885-47d9-824c-db862cc8cc21",
                            Email = "adminuser@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "adminuser@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEPFJ5gjXgLK6QJnLFkLYZ+dpdBi69W5sk4KkHdspU1WcMVuGqEx4FL3Cq3NUpX5feA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d8d76392-3e1a-4ccf-8152-05c9239c61e3",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ddee5992-debf-4a74-925b-6a4b0212afd6",
                            Email = "Css@live.se",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "css@live.se",
                            NormalizedUserName = "felix",
                            PasswordHash = "AQAAAAEAACcQAAAAEC7WMleFTu6blEnrKnV1LfcubMEZfV5Kfi18hS77rhUtzWPqOOiLQJiBqx6vdEQdIQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1d055198-a0d1-41a9-af00-04ad50e7b771",
                            TwoFactorEnabled = false,
                            UserName = "felix"
                        });
                });

            modelBuilder.Entity("N_Chat.Shared.ChatModel", b =>
                {
                    b.HasOne("N_Chat.Shared.UserModel", "User")
                        .WithMany("Chats")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("User");
                });

            modelBuilder.Entity("N_Chat.Shared.MessageModel", b =>
                {
                    b.HasOne("N_Chat.Shared.ChatModel", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("N_Chat.Shared.UserModel", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Chat");

                    b.Navigation("User");
                });

            modelBuilder.Entity("N_Chat.Shared.UserModel", b =>
                {
                    b.HasOne("N_Chat.Shared.ChatModel", null)
                        .WithMany("Users")
                        .HasForeignKey("ChatModelId");
                });

            modelBuilder.Entity("N_Chat.Shared.ChatModel", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("N_Chat.Shared.UserModel", b =>
                {
                    b.Navigation("Chats");

                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
