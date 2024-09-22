using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranquiloSystem.BLL.Dtos.PostDto.PostDto;
using TranquiloSystem.BLL.Dtos.PostDto;
using TranquiloSystem.BLL.Dtos.PostCommentDto;
using TranquiloSystem.BLL.Dtos.GeneralDto;

namespace TranquiloSystem.BLL.Manager.PostCommentManager
{
	public interface IPostCommentManager
	{
		Task<GeneralResponseDto> GetAllAsync();
		Task<GeneralResponseDto> GetByIdAsync(int id);
		Task<GeneralResponseDto> GetByUserIdAsync(string userId);
		Task<GeneralResponseDto> GetByPostIdAsync(int postId);
		Task<GeneralResponseDto> AddAsync(PostCommentAddDto postCommentAddDto);
		Task<GeneralResponseDto> DeleteAsync(int id);
		Task<GeneralResponseDto> UpdateAsync(PostCommentUpdateDto postCommentUpdateDto);

	}
}
