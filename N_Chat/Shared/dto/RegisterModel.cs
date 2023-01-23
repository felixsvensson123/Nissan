using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Chat.Shared.dto
{
    public class RegisterModel : LoginModel
    {
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }
        public string Email { get; set; }   
    }
}
