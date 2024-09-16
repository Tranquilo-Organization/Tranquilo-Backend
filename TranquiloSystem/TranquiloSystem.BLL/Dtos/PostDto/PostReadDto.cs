using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostDto.PostDto
{
    public class PostReadDto
	{
		public string Content { get; set; }
		public int Like { get; set; } = 0;
		public int Comments { get; set; } = 0;
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		public DateTime UpdatedDate { get; set; } = DateTime.Now;
		public string UserId { get; set; }
	}
}

