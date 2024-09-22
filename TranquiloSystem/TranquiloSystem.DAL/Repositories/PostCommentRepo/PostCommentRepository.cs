using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranquilo.DAL.Data.Models;
using TranquiloSystem.DAL.Data.DbHelper;

namespace Tranquilo.DAL.Repositories.CommentRepo
{
	public class PostCommentRepository : IPostCommentRepository
	{
		private readonly TranquiloContext _context;

		public PostCommentRepository(TranquiloContext context)
		{
			_context = context;
		}
		public async Task<IEnumerable<PostComment>> GetAllAsync()
		{
			var comments =  await _context.PostComments
				.Include(u=>u.User).Include(u=>u.Post).AsNoTracking().ToListAsync();
			if (!comments.Any())
			{
				return null;
			}
			return comments;
		}

		public async Task<PostComment> GetByIdAsync(int id)
		{
			var comment =  await _context.PostComments
				.Include(x => x.User).Include(x => x.Post)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == id);
			
			if (comment == null)
			{
				return null;
			}
			return comment;
		}

		public async Task<IEnumerable<PostComment>> GetByUserIdAsync(string userId)
		{
			var comments = await _context.PostComments
							.Where(x => x.UserId == userId)
							.Include(c => c.User).Include(x => x.Post)
							.AsNoTracking().ToListAsync();

			if(comments == null)
			{
				return null; 
			}
			return comments;
		}

		public async Task<IEnumerable<PostComment>> GetByPostIdAsync(int postId)
		{
			var comments = await _context.PostComments
							.Where(x => x.PostID == postId)
							.Include(c => c.User).Include(x => x.Post)
							.AsNoTracking().ToListAsync();

			if(comments == null)
			{
				return null;
			}
			return comments;
		}

		public async Task<int?> AddAsync(PostComment postComment)
		{
			await _context.PostComments.AddAsync(postComment);
			//var posts = await _context.Posts.FirstOrDefaultAsync(x=>x.Id==postComment.PostID);
			//posts.PostComments.Add();

			var result = await _context.SaveChangesAsync();
 			if(result > 0) 
			{
				return postComment.Id;
			}
			//if no row added
			return null;
		}
		public async Task<bool> DeleteAsync(PostComment postComment)
		{
			_context.PostComments.Remove(postComment);
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}
		public async Task<bool> UpdateAsync(PostComment postComment)
		{
			_context.PostComments.Update(postComment);
			var result = await _context.SaveChangesAsync();
			return result > 0;
		}

		
	}
}
