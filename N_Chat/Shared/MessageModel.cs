using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Chat.Shared
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string? Message  { get; set; }
        public string? UserName { get; set; }
        public DateTime MessageCreated { get; set; }
        public DateTime? MessageDeleted { get; set; }
        public DateTime? MessageEdited { get; set; }
        public bool IsMessageEncrypted { get; set; }
        public bool IsMessageEdited { get; set; }
        public bool IsMessageDeleted { get; set; }
        public bool ShowDetails { get; set; }  

        [ForeignKey(nameof(Chat))]
        public int ChatId { get; set; }
        public ChatModel? Chat { get; set; }


        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public UserModel? User { get; set; }
        

    }
}
