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
       public string Email { get; set; }
       public List<MessageModel> Messages { get; set; }
       public List<ChatModel> Chats { get; set; }
    }
}
