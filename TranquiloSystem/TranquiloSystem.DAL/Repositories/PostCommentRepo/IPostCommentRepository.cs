using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace Tranquilo.DAL.Repositories.CommentRepo
{
    public interface IPostCommentRepository
    {
		Task<IEnumerable<PostComment>> GetAllAsync();
		Task<PostComment> GetByIdAsync(int id);
		Task<IEnumerable<PostComment>> GetByUserIdAsync(string userId);
		Task<IEnumerable<PostComment>> GetByPostIdAsync(int postId);

		Task<int?> AddAsync(PostComment postComment);
		Task<bool> DeleteAsync(PostComment postComment);
		Task<bool> UpdateAsync(PostComment postComment);
	}
}
