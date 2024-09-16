using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.DbHelper;

namespace Tranquilo.DAL.Repositories.PostRepo
{
	public class PostRepository : IPostRepository
	{	
		private readonly TranquiloContext _context;

		public PostRepository(TranquiloContext context)
		{
			_context = context;
		}
		public async Task<ICollection<Post>> GetAllAsync()
		{
			return await _context.Posts.AsNoTracking().ToListAsync();
		}
		public async Task<Post> GetByIdAsync(int id)
		{
			return await _context.Posts.FindAsync(id);
		}
		public async Task AddAsync(Post post)
		{
			await _context.AddAsync(post);
		}
		public async Task DeleteAsync(Post post)
		{
			_context.Remove(post);
		}
		public async Task UpdateAsync(Post post)
		{
			
		}
		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
