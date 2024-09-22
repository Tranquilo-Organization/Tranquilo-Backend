using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostCommentDto
{
	public class PostCommentReadDto
	{
		public int Id { get; set; }
		public string CommentText { get; set; }
		public int UpVoteCountLength { get; set; }
		public int DownVoteCountLength { get; set; }
		public DateTime Date { get; set; } 
		public string UserId { get; set; }
		public string UserName { get; set; }
		public int PostID { get; set; }
	}
}
