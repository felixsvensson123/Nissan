namespace N_Chat.Shared
{
	public class ChatModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }

		//Skaparen av chatten
		public string CreatorId { get; set; }
		public bool IsChatEdited { get; set; }
		public bool? IsChatEnded { get; set; }
		public bool IsChatEncrypted { get; set; }
		public bool ShowDetails { get; set; }
		public DateTime ChatCreated { get; set; }
		public DateTime? ChatEnded { get; set; }

		//Lista av meddelanden i chatt
		public virtual ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
		//Lista av användare i chatt
		public virtual ICollection<UserChat> Users { get; set; } = new List<UserChat>();

	}
}
