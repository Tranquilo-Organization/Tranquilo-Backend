using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostCommentDto
{
	public class PostCommentUpdateDto
	{
		public int Id { get; set; }
		public string CommentText { get; set; }
	}
}
