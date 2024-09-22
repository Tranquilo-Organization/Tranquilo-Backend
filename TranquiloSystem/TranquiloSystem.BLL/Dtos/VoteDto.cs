using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos
{
	public class VoteDto
	{
		public int PostId { get; set; }
		public string UserId { get; set; } 
		public bool IsUpVote { get; set; }
	}
}
