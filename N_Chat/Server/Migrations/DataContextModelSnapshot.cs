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
                            Id = "d153c726-e709-4946-824b-0ed63bbf136a",
                            ConcurrencyStamp = "5874cbc3-9e32-424c-8169-4fbdcbd670bc",
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        },
                        new
                        {
                            Id = "d1678ba6-7957-21a7-96b5-12b64c06bc25",
                            ConcurrencyStamp = "0ab63f60-f120-4c6c-98ab-39d16b8b65b5",
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
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                            RoleId = "d1678ba6-7957-21a7-96b5-12b64c06bc25"
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
                            ChatCreated = new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2339),
                            CreatorId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                            IsChatEdited = false,
                            IsChatEncrypted = false,
                            IsChatEnded = false,
                            Name = "CoolChat",
                            UserId = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35"
                        });
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
                            MessageCreated = new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2380),
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
                            MessageCreated = new DateTime(2023, 1, 31, 0, 43, 0, 574, DateTimeKind.Local).AddTicks(2386),
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
                            MessageCreated = new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2389),
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
                            MessageCreated = new DateTime(2023, 1, 30, 19, 43, 0, 574, DateTimeKind.Local).AddTicks(2391),
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
                        .IsRequired()
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
                            ConcurrencyStamp = "d03c5159-3441-4e22-837d-a1684a642b7c",
                            Email = "adminuser@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "adminuser@gmail.com",
                            NormalizedUserName = "admin",
                            PasswordHash = "AQAAAAEAACcQAAAAEE8Chgs/SB3JdliyyMLufSyNeqwCTAQQjgxAcbNiFSszba54K20ccKAkyvTVOLN+tQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "08c39a92-c3ab-4b92-8ef7-51efab503871",
                            TwoFactorEnabled = false,
                            UserName = "admin"
                        },
                        new
                        {
                            Id = "d7fc4ba6-4957-41a7-96b5-52b65c06bc35",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ecf82a42-7fe1-46f5-b051-48653093fc0f",
                            Email = "Css@live.se",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "css@live.se",
                            NormalizedUserName = "felix",
                            PasswordHash = "AQAAAAEAACcQAAAAEEW7iNWCLBX2xlKz0m7vgZHevCfrtY+fMv52xROJ9gAnFmwmmksuGR+4+vyrrybImA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a8108c51-f6e9-4a23-b2dd-1ba8abee6d4e",
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
