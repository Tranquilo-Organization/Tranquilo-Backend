using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostManager
{
	public interface IPostManager
	{
		Task<ICollection<PostReadDto>> GetAllAsync();
        PostReadDto GetById(int id);
        Task Add(PostAddDto postAddDto);
        Task Update(PostUpdateDto postUpdateDto);
        Task Delete(int id);
        Task SaveChangeAsync();
    }
}
