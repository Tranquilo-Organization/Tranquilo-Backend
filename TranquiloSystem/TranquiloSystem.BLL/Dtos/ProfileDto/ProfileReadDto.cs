using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranquiloSystem.BLL.Dtos.ProfileDto
{
	public class ProfileReadDto
	{
		public string Id { get; set; }
		public string ProfilePicture { get; set; }
		public string NickName { get; set; } 
		public string UserName { get; set; }
		public string Email { get; set; }
		public string LevelName { get; set; }
        
    }
}
