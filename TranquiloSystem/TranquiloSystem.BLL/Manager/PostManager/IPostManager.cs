using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.BLL.Dtos;
using TranquiloSystem.BLL.Dtos.GeneralDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;

namespace TranquiloSystem.BLL.Manager.PostManager
{
	public interface IPostManager
	{
		Task<GeneralResponseDto> GetAllAsync();
		Task<GeneralResponseDto> GetByIdAsync(int id);
		Task<GeneralResponseDto> GetByUserIdAsync(string userId);
		Task<GeneralResponseDto> AddAsync(PostAddDto postAddDto);
		Task<GeneralResponseDto> DeleteAsync(int id);
		Task<GeneralResponseDto> UpdateAsync(PostUpdateDto postUpdateDto);

		Task<GeneralResponseDto> VoteAsync(VoteDto voteDto);
	}
}
