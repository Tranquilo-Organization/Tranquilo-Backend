using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
	public class ChatBotInteraction
	{
		public int Id { get; set; }
		public string UserMessage { get; set; }
		public string BotMessage { get; set; }
		public DateTime InteractionDate { get; set; }

		public string? UserId { get; set; }
		public ApplicationUser User { get; set; }
	}

}
