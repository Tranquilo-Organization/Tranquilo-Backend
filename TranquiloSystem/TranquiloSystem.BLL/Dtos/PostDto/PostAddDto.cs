﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.PostDto
{
    public class PostAddDto
    {
		public string PostText { get; set; }
		public string UserEmail { get; set; }
	}
}

