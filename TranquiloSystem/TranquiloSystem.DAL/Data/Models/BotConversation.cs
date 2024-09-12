using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.DAL.Data.Models;

namespace Tranquilo.DAL.Data.Models
{
	public class BotConversation
	{
		public int Id { get; set; }
		public string? UserId { get; set; }
		public ApplicationUser User { get; set; }
		public List<Message> Messages { get; set; } = new List<Message>();

	}

}
