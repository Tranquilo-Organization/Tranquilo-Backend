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
        Task<ICollection<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(int id);
		Task DeleteAsync(Post post);
		Task UpdateAsync(Post post);
		Task AddAsync(Post post);
		Task SaveChangesAsync();

	}
}
