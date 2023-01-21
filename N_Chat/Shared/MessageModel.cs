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
        [Key]
        public int Id { get; set; }
        public string? Message  { get; set; }
        public DateTime MessageCreated { get; set; }
        public DateTime? MessageDeleted { get; set; }
        public DateTime? MessageEdited { get; set; }
        public bool IsMessageEncrypted { get; set; }
        public bool IsMessageEdited { get; set; }
        public bool IsMessageDeleted { get; set; }

        [ForeignKey(nameof(Chat))]

        public int ChatId { get; set; }
        public ChatModel? Chat { get; set; }


        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public UserModel? User { get; set; }
        

    }
}
