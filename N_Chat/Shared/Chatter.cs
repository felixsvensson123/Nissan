using System.ComponentModel.DataAnnotations;

namespace N_Chat.Shared;

public class Chatter
{
    [Key]
    public string? Id { get; set; }
    public string? Username { get; set; }
}