using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace N_Chat.Shared
{
    public class UserModel : IdentityUser
    { 
        public string? Email { get; set; }
       public virtual ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
       public virtual ICollection<UserChat> Chats { get; set; } = new List<UserChat>();
        public bool? isDeleted { get; set; }
    }
}
