using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace TranquiloSystem.BLL.Dtos.ProfileDto
{
    public class ProfileUpdateDto
    {
		public string? ProfilePicture { get; set; }
		public string? UserName { get; set; }
        public string? NickName { get; set; }
		public string? Email { get; set;}
	}
}
