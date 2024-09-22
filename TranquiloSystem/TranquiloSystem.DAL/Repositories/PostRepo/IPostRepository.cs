using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;

namespace Tranquilo.DAL.Repositories.PostRepo
{	
	public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
		Task<IEnumerable<Post>> GetByUserIdAsync(string userId);
		Task<int?> AddAsync(Post post);
		Task<bool> DeleteAsync(Post post);
		Task<bool> UpdateAsync(Post post);		
	}
}
