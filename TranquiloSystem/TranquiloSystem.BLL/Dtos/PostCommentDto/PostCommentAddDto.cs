using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostCommentDto
{
	public class PostCommentAddDto
	{
		public string CommentText { get; set; }
		public string UserEmail { get; set; }
		public int PostID { get; set; }
	}
}
