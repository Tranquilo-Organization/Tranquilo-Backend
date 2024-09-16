using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostManager
{
	public interface IPostManager
	{
		Task<ICollection<PostReadDto>> GetAllAsync();
	}
}
