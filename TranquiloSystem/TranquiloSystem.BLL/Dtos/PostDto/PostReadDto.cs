﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostDto.PostDto
{
    public class PostReadDto
	{
		public int Id { get; set; }
		public string PostText { get; set; }
		public int UpVoteCountLength { get; set; }
		public int DownVoteCountLength { get; set; }
		public int CommentsCount { get; set; } = 0;
		public DateTime Date { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
	}
}

