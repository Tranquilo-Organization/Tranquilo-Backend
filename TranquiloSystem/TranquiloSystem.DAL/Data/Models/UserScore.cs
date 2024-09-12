using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
	public class UserScore
	{
		public int Id { get; set; } 
		public string UserId { get; set; } 
		public ApplicationUser User { get; set; }
		public int Score { get; set; } 
		public DateTime LastUpdated { get; set; }
		public int LevelId { get; set; } 
		public Level Level { get; set; } 
	}
}
