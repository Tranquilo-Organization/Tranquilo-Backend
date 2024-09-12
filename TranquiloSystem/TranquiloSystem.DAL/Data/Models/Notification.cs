using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.DAL.Data.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public bool IsRead { get; set; } = false;

		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}
