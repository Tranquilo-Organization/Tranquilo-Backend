using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranquilo.DAL.Data.Models
{
    public class Post
    {
		public int Id { get; set; }
		public string PostText { get; set; }
		public List<string> UpVoteCount { get; set; } = new List<string>();
		public int UpVoteCountLength => UpVoteCount?.Count ?? 0; 
		public List<string> DownVoteCount { get; set; } = new List<string>();
		public int DownVoteCountLength => DownVoteCount?.Count ?? 0;
		public DateTime Date { get; set; } = DateTime.Now;
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public List<PostComment> PostComments { get; set; }
	}
}
