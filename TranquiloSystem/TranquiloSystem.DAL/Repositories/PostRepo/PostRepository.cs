using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
		public async Task<IEnumerable<Post>> GetAllAsync()
		{
			var posts = await _context.Posts.
				Include(c => c.User).Include(x => x.PostComments)
				.AsNoTracking().ToListAsync();

			if (!posts.Any())
			{
				return null;
			}
			return posts;
		}
		public async Task<Post> GetByIdAsync(int id)
		{
			var post = await _context.Posts
				.Include(x => x.User).Include(x => x.PostComments)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);

			if (post == null)
			{
				return null;
			}
			return post;
		}

		public async Task<IEnumerable<Post>> GetByUserIdAsync(string userId)
		{
			var posts = await _context.Posts
							.Where(x => x.UserId == userId)
							.Include(c => c.User).Include(x => x.PostComments)
							.AsNoTracking().ToListAsync();

			if (posts == null)
			{
				return null;
			}
			return posts;
		}
		public async Task<int?> AddAsync(Post post)
		{
			await _context.Posts.AddAsync(post);

			var result = await _context.SaveChangesAsync();
			if (result > 0)
			{
				return post.Id;
			}
			//if no row added
			return null;
		}
		public async Task<bool> DeleteAsync(Post post)
		{
			_context.Posts.Remove(post);
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}
		public async Task<bool> UpdateAsync(Post post)
		{
			_context.Posts.Update(post);
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}

	
	}
}
