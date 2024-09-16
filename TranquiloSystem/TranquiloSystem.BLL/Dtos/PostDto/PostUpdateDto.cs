using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostDto
{
	public class PostUpdateDto
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime UpdatedDate { get; set; } = DateTime.Now;
	}
}

