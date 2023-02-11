using System.ComponentModel.DataAnnotations;

namespace N_Chat.Shared;

public class UserChat
{
    public string? UserId { get; set; }
    public UserModel? User { get; set; }

    public int ChatId { get; set; }
    public ChatModel? Chat { get; set; }
    
}