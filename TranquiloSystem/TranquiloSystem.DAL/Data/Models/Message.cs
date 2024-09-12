using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.DAL.Data.Models
{
	 public class Message
	{
		public int Id { get; set; }
		public int BotConversationId { get; set; }
		public BotConversation BotConversation { get; set; }
		public string Content { get; set; } //from user or bot
		public DateTime Timestamp { get; set; } = DateTime.Now;
		public bool IsFromUser { get; set; } //true if from user, false if it from bot
	}
}
